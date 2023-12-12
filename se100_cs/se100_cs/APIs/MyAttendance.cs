using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using Serilog;
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
            using (DataContext context = new DataContext())
            {
                SqlAttendance atd = context.attendances.Where(s => s.year == year && s.month == month && s.day == day).Include(s => s.list_attendance).FirstOrDefault();
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
                    SqlEmployee? employee = context.employees!.Where(s => s.isDeleted == false && s.ID == detail.employeeId).Include(s => s.department).FirstOrDefault();
                    if (employee == null)
                    {
                        continue;
                    }
                    item.emp_name = employee.fullName;
                    item.department_code = employee.department.code;
                    response.Add(item);
                }
            }
            return response;
        }

        public string attendance_status(int i)
        {
            if (i == 0) return "OnTime";// trả thêm cái tên
            if (i == 1) return "Late";
            return "Absent";
        }
        public async Task<bool> init_attendance_async()
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

                attendance = context.attendances!.Where(s => s.year == DateTime.Now.Year && s.month == DateTime.Now.Month && s.day == now.Day).Include(s => s.list_attendance).FirstOrDefault();
                if (attendance.list_attendance != null && !attendance.list_attendance.Any())
                {
                    List<SqlEmployee>? allEmployees = context.employees.Where(s => s.isDeleted == false).ToList();
                    foreach (SqlEmployee emp in allEmployees)
                    {
                        SqlATDDetail detail = new SqlATDDetail();
                        detail.employeeId = emp.ID;
                        detail.attendance = attendance;
                        context.attendance_details.Add(detail);
                    }
                    await context.SaveChangesAsync();
                }
            }
            return true;
        }
        public async Task<string> markAttendance(long employee_id)
        {
            DateTime now = DateTime.Now;
            SqlSetting? setting = new SqlSetting();
            using (DataContext context = new DataContext())
            {
                    setting = context.settings!.FirstOrDefault();
                int start_time_hour = setting.start_time_hour;
                int start_time_minute = setting.start_time_minute;

                SqlEmployee? employee = context.employees!.Where(s => s.ID == employee_id).FirstOrDefault();
                if (employee == null)
                {
                    return "employee null";
                }
                 

                await init_attendance_async();


                SqlAttendance? attendance = context.attendances!.Where(s => s.year == DateTime.Now.Year && s.month == DateTime.Now.Month && s.day == now.Day).Include(s => s.list_attendance).FirstOrDefault();

                SqlATDDetail? existing = attendance.list_attendance.Where(s => s.employeeId == employee.ID).FirstOrDefault();
                if (existing.time.CompareTo(new TimeOnly(1, 1, 1)) == 0)
                {
                    if (now.Hour * 60 + now.Minute > start_time_hour * 60 + start_time_minute)
                    {
                        existing.status = 1;
                        existing.time = TimeOnly.FromDateTime(now);
                    }
                    else
                    {
                        existing.status = 0;
                        existing.time = TimeOnly.FromDateTime(now);
                    }
                }
                await context.SaveChangesAsync();
                return attendance_status(existing.status);
            }
        }
        public string get_attendance_status(string token)
        {
            DateTime now = DateTime.Now;
            using (DataContext context = new DataContext())
            {

            }
            return "";
        }
        public class Employees_Today
        {
            public int thu { get; set; }
            public int ngay { get; set; }
            public int attendance { get; set; } = 0;
            public int late_coming { get; set; } = 0;
            public int absent { get; set; } = 0;

        }

        //public List<Employees_Today> getEmployees_ByWeek()
        //{
        //    int ngay_hom_nay = DateTime.Now.Day;
        //    int thang_truoc = DateTime.Now.Month - 1;
        //    List<Employees_Today> week = new List<Employees_Today>();
        //    int hom_nay_la_thu = (int)DateTime.Now.DayOfWeek + 1; // vi du thu 5, thứ 2 là bằng 2
        //    if (hom_nay_la_thu == 1) { hom_nay_la_thu = 8; }
        //    for (int i = hom_nay_la_thu; i >= 2; i--)
        //    {
        //        Employees_Today today = new Employees_Today();
        //        using (DataContext context = new DataContext())
        //        {


        //            if (ngay_hom_nay - (hom_nay_la_thu - i) < 1)
        //            {
        //                if (thang_truoc % 2 == 1)
        //                {
        //                    ngay_hom_nay = 30;
        //                }
        //                else
        //                {
        //                    ngay_hom_nay = 31;
        //                }
        //                hom_nay_la_thu = i;
        //            }
        //            int attendance = context.attendances!.Where(s => s.status == 0 && s.time.Day == ngay_hom_nay - (hom_nay_la_thu - i)).Count();
        //            int late_coming = context.attendances!.Where(s => s.status == 1 && s.time.Day == ngay_hom_nay - (hom_nay_la_thu - i)).Count();
        //            int total = Program.api_employee.countTotalEmployee();
        //            int absent = total - attendance - late_coming;
        //            today.thu = i;
        //            today.ngay = ngay_hom_nay - (hom_nay_la_thu - i);
        //            today.attendance = attendance;
        //            today.absent = absent;
        //            today.late_coming = late_coming;
        //            week.Add(today);
        //        }
        //    }
        //    week = week.OrderBy(s => s.thu).ToList();
        //    return week;
        //}
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
        //public Employees_Today getEmployees_Today()
        //{
        //    int day = DateTime.Now.Day;
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
    }
}
