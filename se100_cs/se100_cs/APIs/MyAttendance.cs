using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using se100_cs.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace se100_cs.APIs
{
    public class MyAttendance
    {
        public MyAttendance() { }
        public class Attendance_DTO_Response
        {
            public int hour { get; set; }
            public int minute { get; set; }
            public string status { get; set; } = "Absent";
            public string emp_name { get; set; }
            public string department_code { get; set; }
        }
        public List<Attendance_DTO_Response> getListByDate(int day, int month, int year)
        {
            List<Attendance_DTO_Response> response = new List<Attendance_DTO_Response>();
            if (year >= DateTime.Now.Year)
            {
                if (month >= DateTime.Now.Month)
                {
                    if (day > DateTime.Now.Day)
                    {
                        return response;
                    }
                }
            }
            using (DataContext context = new DataContext())
            {
                SqlAttendance atd = context.attendances.Where(s => s.year == year && s.month == month && s.day == day).Include(s => s.list_attendance).ThenInclude(s => s.employee).ThenInclude(s => s.department).FirstOrDefault();
                if (atd == null)
                {
                    return response;
                }
                if (atd.list_attendance == null) { return response; }
                List<SqlATDDetail> list_detail = atd.list_attendance;
                foreach (SqlATDDetail detail in list_detail)
                {
                    Attendance_DTO_Response item = new Attendance_DTO_Response();
                    item.hour = detail.time.Hour;
                    item.minute = detail.time.Minute;
                    item.status = attendance_status(detail.status);
                    item.emp_name = detail.employee.fullName;
                    item.department_code = detail.employee.department.code;
                    response.Add(item);
                }
                response = response.OrderBy(s => s.hour).ToList();

            }
            return response;
        }

        public string attendance_status(int i)
        {
            if (i == 0) return "OnTime";// trả thêm cái tên
            if (i == 1) return "Late";
            return "Absent";
        }
        public async Task init_attendance_today_async()
        {
            DateTime now = DateTime.Now;
            using (DataContext context = new DataContext())
            {
                SqlAttendance? attendance = context.attendances!.Where(s => s.year == DateTime.Now.Year && s.month == DateTime.Now.Month && s.day == now.Day).Include(s => s.list_attendance).FirstOrDefault();
                if (attendance == null)
                {
                    SqlAttendance tmp = new SqlAttendance();
                    tmp.year = now.Year;
                    tmp.month = now.Month;
                    tmp.day = now.Day;
                    tmp.list_attendance = new List<SqlATDDetail>();
                    context.attendances.Add(tmp);
                    await context.SaveChangesAsync();
                }

                attendance = context.attendances!.Where(s => s.year == now.Year && s.month == now.Month && s.day == now.Day).Include(s => s.list_attendance).FirstOrDefault();
                if (attendance.list_attendance != null && !attendance.list_attendance.Any())
                {
                    List<SqlEmployee>? allEmployees = context.employees.Where(s => s.isDeleted == false).ToList();
                    foreach (SqlEmployee emp in allEmployees)
                    {
                        SqlATDDetail detail = new SqlATDDetail();
                        detail.employee = emp;
                        detail.attendance = attendance;
                        context.attendance_details.Add(detail);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task<string> markAttendance(long employee_id)
        {
            Status_Response response = new Status_Response();
            DateTime now = DateTime.Now;
            using (DataContext context = new DataContext())
            {
                SqlSetting? setting = context.settings!.FirstOrDefault();
                int start_time_hour = setting.start_time_hour;
                int start_time_minute = setting.start_time_minute;

                SqlEmployee? employee = context.employees!.Where(s => s.ID == employee_id).Include(s => s.department).FirstOrDefault();
                if (employee == null)
                {
                    return "employee null";
                }
                response.fullName = employee.fullName;
                response.department = employee.department.name;
                await init_attendance_today_async();


                SqlAttendance? attendance = context.attendances!.Where(s => s.year == now.Year && s.month == now.Month && s.day == now.Day).Include(s => s.list_attendance).FirstOrDefault();

                SqlATDDetail? existing = attendance.list_attendance.Where(s => s.employee == employee).FirstOrDefault();
                if (existing == null)
                {
                    return JsonConvert.SerializeObject(response);
                }
                if (existing.time.CompareTo(new TimeOnly(23, 59, 0)) == 0)
                {
                    if (now.Hour * 60 + now.Minute > start_time_hour * 60 + start_time_minute)
                    {//late
                        existing.status = 1;
                        existing.time = TimeOnly.FromDateTime(now);
                        response.time = existing.time;
                        response.status = attendance_status(existing.status);
                        // gọi signalr
                    }
                    else
                    {//ontime
                        existing.status = 0;
                        existing.time = TimeOnly.FromDateTime(now);
                        response.time = existing.time;
                        response.status = attendance_status(existing.status);
                        // gọi signalr
                    }
                }
                else
                {
                    response.status = attendance_status(existing.status);
                    response.time = existing.time;
                }
                await context.SaveChangesAsync();
                return JsonConvert.SerializeObject(response);
            }
        }

        public class Status_Response
        {
            public string status;//late, on time, absent
            public string fullName;
            public TimeOnly time;
            public string department;
            //public 
        }
        public async Task<string> check(string token)
        {
            Status_Response response = new Status_Response();
            DateTime now = DateTime.Now;
            using (DataContext context = new DataContext())
            {
                SqlEmployee? employee = context.employees!.Where(s => s.token == token).Include(s => s.department).FirstOrDefault();
                if (employee == null)
                {
                    return JsonConvert.SerializeObject(response);
                }
                response.fullName = employee.fullName;
                await init_attendance_today_async();
                SqlAttendance? attendance = context.attendances!.Where(s => s.year == now.Year && s.month == now.Month && s.day == now.Day).Include(s => s.list_attendance).FirstOrDefault();
                SqlATDDetail? existing = attendance.list_attendance.Where(s => s.employee == employee).FirstOrDefault();
                if (existing == null)
                {
                    response.status = attendance_status(2);
                    return JsonConvert.SerializeObject(response);
                }
                response.status = attendance_status(existing.status);
                response.time = existing.time;
                response.department = employee.department.name;
            }
            return JsonConvert.SerializeObject(response);
        }
        public class Employees_Today
        {
            public int thu { get; set; }
            public int ngay { get; set; }
            public int on_time { get; set; } = 0;
            public int late_coming { get; set; } = 0;
            public int absent { get; set; } = 0;

        }

        public List<Employees_Today> getEmployees_ByWeek()
        {
            int ngay_hom_nay = DateTime.Now.Day;
            int thang_truoc = DateTime.Now.Month - 1;
            List<Employees_Today> response = new List<Employees_Today>();
            //int hom_nay_la_thu = (int)DateTime.Now.DayOfWeek + 1; // vi du thu 5, thứ 2 là bằng 2
            //if (hom_nay_la_thu == 1) { hom_nay_la_thu = 8; }
            //for (int i = hom_nay_la_thu; i >= 2; i--)
            //{
            //    Employees_Today today = new Employees_Today();
            //    using (DataContext context = new DataContext())
            //    {


            //        if (ngay_hom_nay - (hom_nay_la_thu - i) < 1)
            //        {
            //            if (thang_truoc % 2 == 1)
            //            {
            //                ngay_hom_nay = 30;
            //            }
            //            else
            //            {
            //                ngay_hom_nay = 31;
            //            }
            //            hom_nay_la_thu = i;
            //        }
            //        int attendance = context.attendances!.Where(s => s.status == 0 && s.time.Day == ngay_hom_nay - (hom_nay_la_thu - i)).Count();
            //        int late_coming = context.attendances!.Where(s => s.status == 1 && s.time.Day == ngay_hom_nay - (hom_nay_la_thu - i)).Count();
            //        int total = Program.api_employee.countTotalEmployee();
            //        int absent = total - attendance - late_coming;
            //        today.thu = i;
            //        today.ngay = ngay_hom_nay - (hom_nay_la_thu - i);
            //        today.attendance = attendance;
            //        today.absent = absent;
            //        today.late_coming = late_coming;
            //        response.Add(today);
            //    }
            //}
            response = response.OrderBy(s => s.thu).ToList();
            return response;
        }


        //public Employees_Today getEmployees_Byday(int day)
        //{
        //    Employees_Today today = new Employees_Today();
        //    int count_attendance = 0;
        //    using (DataContext context = new DataContext())
        //    {
        //        int attendance = context.attendances.Where(s => s.status == 0 && s.time.Day == day).Count();
        //        int late_coming = context.attendances!.Where(s => s.status == 1 && s.time.Day == day).Count();
        //        int total = Program.api_employee.countTotalEmployee();
        //        int absent = total - attendance - late_coming;
        //        today.attendance = attendance;
        //        today.absent = absent;
        //        today.late_coming = late_coming;
        //    }
        //    return today;
        //}


        public Employees_Today getEmployees_Today()
        {
            DateTime today = DateTime.Now;
            Employees_Today response = new Employees_Today();
            int count_attendance = 0;
            using (DataContext context = new DataContext())
            {
                SqlAttendance? attendance = context.attendances.Where(s => s.day == today.Day && s.month == today.Month && s.year == today.Year).Include(s => s.list_attendance).FirstOrDefault();
                if (attendance == null)
                {
                    //create attendance today
                }
                else
                {
                    int late_coming = attendance.list_attendance.Where(s => s.status == 1).Count();
                    int on_time = attendance.list_attendance.Where(s => s.status == 0).Count();
                    int absent = attendance.list_attendance.Where(s => s.status == 2).Count();
                    response.on_time = on_time;
                    response.absent = absent;
                    response.late_coming = late_coming;
                }
            }
            return response;
        }

        public async Task<string> update_attendance_admin(int status)
        {
            int day = DateTime.UtcNow.Day, month = DateTime.UtcNow.Month, year = DateTime.UtcNow.Year;
            using (DataContext context = new DataContext())
            {// thắng
                SqlATDDetail? admin_today = context.attendance_details.Include(s => s.attendance).Where(s => s.employee.ID == 999999999 && s.attendance.day == day).Include(s => s.employee).FirstOrDefault();
                if (admin_today == null)
                {
                    await init_attendance_today_async();
                    admin_today = context.attendance_details.Include(s => s.attendance).Where(s => s.employee.ID == 999999999 && s.attendance.day == day).Include(s => s.employee).FirstOrDefault();
                }
                int old_status = admin_today.status;
                if (old_status != status)
                {
                    admin_today.status = status;
                    if (admin_today.status == 0)
                    {
                        admin_today.time = new TimeOnly(8, 15, 0);
                    }
                    else if (admin_today.status == 1)
                    {
                        admin_today.time = new TimeOnly(8, 35, 0);
                    }
                    else
                    {
                        admin_today.time = new TimeOnly(23, 59, 0);
                    }
                    await context.SaveChangesAsync();
                }
                return attendance_status(admin_today.status);
            }
        }

        public class History_Day_ATD
        {
            public int day { get; set; }
            public int month { get; set; }
            public int year { get; set; }
            public List<History_Day_Item> list_item { get; set; }

        }
        public class History_Day_Item
        {
            public string fullName { get; set; } = "";
            public string avatar { get; set; } = "";
            public TimeOnly time { get; set; }
            public int status { get; set; } = 2;
            // 0 la dung gio, 1 la tre gio
        }
        public List<History_Day_ATD> getAll(int limit_day)
        {
            List<History_Day_ATD> response = new List<History_Day_ATD>();
            DateTime today = DateTime.UtcNow;
            using (DataContext context = new DataContext())
            {
                List<SqlAttendance>? attendances = context.attendances!.Where(s => s.day * s.month <= today.Day * today.Month)
                                        .OrderByDescending(s => s.day * s.month).Take(limit_day).Include(s => s.list_attendance).ThenInclude(s => s.employee).ToList();
                if (attendances.Any())
                {
                    foreach (SqlAttendance attendance in attendances)
                    {
                        History_Day_ATD item = new History_Day_ATD();
                        item.day = attendance.day;
                        item.month = attendance.month;
                        item.year = attendance.year;
                        List<History_Day_Item> list_item = new List<History_Day_Item>();
                        if (attendance.list_attendance.Any())
                        {
                            attendance.list_attendance = attendance.list_attendance.Where(s => s.status != 2).ToList();
                            foreach (SqlATDDetail tmp in attendance.list_attendance)
                            {
                                History_Day_Item history_Day_Item = new History_Day_Item();
                                history_Day_Item.time = tmp.time;
                                history_Day_Item.fullName = tmp.employee.fullName;
                                history_Day_Item.avatar = tmp.employee.avatar;
                                history_Day_Item.status = tmp.status;
                                list_item.Add(history_Day_Item);
                            }
                            list_item= list_item.OrderByDescending(s => s.time).ToList();
                            item.list_item=list_item;
                            response.Add(item);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    return response;
                }
            }
            return response;
        }
    }
}