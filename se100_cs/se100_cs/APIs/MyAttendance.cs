using MessagePack.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using se100_cs.Controllers.Attendance.ResponseDTO;
using se100_cs.Controllers.Dashboard.Response_DTO;
using se100_cs.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace se100_cs.APIs
{
    public class MyAttendance
    {

        public MyAttendance() { }
        public async Task SendNotiBackgroundTask()
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee qv = context.employees
                .Where(s => s.ID == 1)
                .FirstOrDefault();

                if (qv != null)
                {
                    bool tmp = Program.api_attendance.getListNotiSignalR(qv.IdHub);

                    if (tmp)
                    {
                        Log.Information("Send message ok");
                    }
                    else
                    {
                        Log.Information("Send message fail");
                    }
                }
            }
        }
        public async Task<Attendance_RES> markAttendance(long user_id)
        {
            DateTime today = DateTime.UtcNow;
            Attendance_RES response = new Attendance_RES();
            using (DataContext context = new DataContext())
            {
                SqlEmployee? emp = context.employees!.Where(s => s.ID == user_id).Include(s => s.attendances!).ThenInclude(s => s.state).Include(s => s.department).FirstOrDefault();
                if (emp == null)
                {
                    return response;
                }
                response.employee_name = emp.fullName;
                if (emp.department == null)
                {
                    return response;
                }
                response.department_name = emp.department.name;
                if (!emp.attendances!.Any())
                {
                    List<SqlAttendance> list = new List<SqlAttendance>();
                    SqlAttendance newATD = new SqlAttendance();
                    newATD.ID = DataContext.Generate_UID();
                    newATD.department = emp.department;
                    newATD.employee = emp;
                    newATD.time = today.AddHours(7);
                    string state_code = Program.api_state.getState(newATD.time);
                    SqlState? state = context.ATD_state.Where(s => s.code.CompareTo(state_code) == 0).FirstOrDefault();
                    if (state == null)
                    {
                        return response;
                    }
                    newATD.state = state;
                    context.attendances!.Add(newATD);
                    list.Insert(0, newATD);
                    emp.attendances = list;
                    response.time = newATD.time;
                    response.attendance_state = state_code;
                    await context.SaveChangesAsync();


                    //send signalr

                    Task.Run(() => SendNotiBackgroundTask());

                    //
                    return response;
                }
                SqlAttendance? sqlAttendance = emp.attendances!.First();
                if (today.Date == sqlAttendance.time.Date)
                {
                    response.time = sqlAttendance.time;
                    response.attendance_state = sqlAttendance.state!.code;
                    return response;
                }
                else
                {
                    SqlAttendance newATD = new SqlAttendance();
                    newATD.ID = DataContext.Generate_UID();
                    newATD.employee = emp;
                    newATD.time = today.AddHours(7);
                    newATD.department = emp.department;
                    string state_code = Program.api_state.getState(newATD.time);
                    SqlState? state = context.ATD_state.Where(s => s.code.CompareTo(state_code) == 0).FirstOrDefault();
                    if (state == null)
                    {
                        return response;
                    }
                    newATD.state = state;
                    context.attendances!.Add(newATD);
                    emp.attendances!.Insert(0, newATD);
                    response.time = sqlAttendance.time;
                    response.attendance_state = sqlAttendance.state!.code;
                    await context.SaveChangesAsync();

                    Task.Run(() => SendNotiBackgroundTask());

                    return response;
                }

            }

        }

        public Attendance_RES check(long user_id)
        {
            DateTime today = DateTime.UtcNow;
            Attendance_RES response = new Attendance_RES();
            using (DataContext context = new DataContext())
            {
                SqlEmployee? emp = context.employees!.Where(s => s.ID == user_id).Include(s => s.attendances!).ThenInclude(s => s.state).Include(s => s.department).AsNoTracking().FirstOrDefault();
                if (emp == null)
                {
                    return response;
                }
                response.employee_name = emp.fullName;
                if (emp.department == null)
                {
                    return response;
                }
                response.department_name = emp.department.name;
                if (!emp.attendances!.Any())
                {
                    response.time = new DateTime(1, 1, 1, 23, 59, 59);
                    response.attendance_state = "Absent";
                    return response;
                }

                SqlAttendance? sqlAttendance = emp.attendances!.First();
                if (emp.attendances!.Any())
                {
                    if (today.Date == sqlAttendance.time.Date)
                    {
                        response.time = sqlAttendance.time;
                        response.attendance_state = sqlAttendance.state!.code;
                        return response;
                    }
                    else
                    {
                        response.time = new DateTime(1, 1, 1, 23, 59, 59);
                        response.attendance_state = "Absent";
                        return response;
                    }
                }
                else
                {
                    response.time = new DateTime(1, 1, 1, 23, 59, 59);
                    response.attendance_state = "Absent";
                    return response;
                }
            }
        }
        public Dashboard_Attendance_RES getEmployees_Today()
        {
            Dashboard_Attendance_RES today = new Dashboard_Attendance_RES();
            int ngay_hom_nay = DateTime.Now.Day;
            int hom_nay_la_thu = (int)DateTime.Now.DayOfWeek + 1;
            if (hom_nay_la_thu == 1) { hom_nay_la_thu = 8; }
            today.thu = hom_nay_la_thu;
            today.ngay = ngay_hom_nay;
            today.attendance = count_onTime_all_company(DateTime.Today);
            today.late = count_Late_all_company(DateTime.Today);
            today.absent = Program.api_employee.countTotalEmployee() - today.attendance - today.late;
            return today;
        }

        public int count_onTime_all_company(DateTime date)
        {
            int count = 0;
            using (DataContext context = new DataContext())
            {
                count = context.attendances!.Where(s => s.time.Date == DateTime.UtcNow.Date && s.state!.code.CompareTo("OnTime") == 0).ToList().Count();
            }
            return count;
        }
        public int count_Late_all_company(DateTime date)
        {
            int count = 0;
            using (DataContext context = new DataContext())
            {
                count = context.attendances!.Where(s => s.time.Date == DateTime.UtcNow.Date && s.state!.code.CompareTo("Late") == 0).ToList().Count();
            }
            return count;
        }
        public List<Dashboard_Attendance_RES> getEmployees_ByWeek()
        {
            List<Dashboard_Attendance_RES> response = new List<Dashboard_Attendance_RES>();
            int year = DateTime.UtcNow.Year;
            int ngay_hom_nay = DateTime.Now.Day;
            int thang = DateTime.Now.Month;
            int thang_truoc = DateTime.Now.Month - 1;
            int hom_nay_la_thu = (int)DateTime.Now.DayOfWeek + 1; // vi du thu 5, thứ 2 là bằng 2
            if (hom_nay_la_thu == 1) { hom_nay_la_thu = 8; }
            for (int i = hom_nay_la_thu; i >= 2; i--)
            {
                Dashboard_Attendance_RES today = new Dashboard_Attendance_RES();
                using (DataContext context = new DataContext())
                {
                    if (ngay_hom_nay - (hom_nay_la_thu - i) < 1)
                    {
                        if (thang_truoc % 2 == 1)
                        {
                            ngay_hom_nay = 30;
                        }
                        else
                        {
                            ngay_hom_nay = 31;
                        }
                        hom_nay_la_thu = i;
                        thang -= 1;
                    }
                    DateTime date = DateTime.UtcNow.AddDays(-(hom_nay_la_thu - i));
                    List<SqlAttendance>? list_attendance = context.attendances!.Include(s => s.state).Where(s => s.time.Date == date.Date).ToList();
                    int on_time = list_attendance.Where(s => s.state.code.CompareTo("OnTime") == 0).Count();
                    int late_coming = list_attendance.Where(s => s.state.code.CompareTo("Late") == 0).Count();
                    int absent = Program.api_employee.countTotalEmployee() - on_time - late_coming;
                    today.thu = i;
                    today.ngay = ngay_hom_nay - (hom_nay_la_thu - i);
                    today.attendance = on_time;
                    today.absent = absent;
                    today.late = late_coming;
                    response.Add(today);
                }
            }
            response = response.OrderBy(s => s.thu).ToList();
            return response;
        }

        public List<Item_Attendance_Res> getList(string day, string department_code)
        {
            List<Item_Attendance_Res> response = new List<Item_Attendance_Res>();
            DateTime date = DateTime.Parse(day);
            using (DataContext context = new DataContext())
            {
                List<SqlAttendance> list;
                if (department_code.CompareTo("all") == 0)
                {
                    list = context.attendances.Where(s => s.time.Day.CompareTo(date.Day) == 0).Include(s => s.employee).Include(s => s.department).Include(s => s.state).ToList();
                }
                else
                {
                    list = context.attendances.Where(s => s.time.Day.CompareTo(date.Day) == 0
                                            && s.department.code.CompareTo(department_code) == 0).Include(s => s.employee).Include(s => s.department).Include(s => s.state).ToList();
                }
                foreach (SqlAttendance attendance in list)
                {
                    Item_Attendance_Res item = new Item_Attendance_Res();
                    item.employee_name = attendance.employee.fullName;
                    item.avatar = attendance.employee.avatar;
                    item.time = attendance.time;
                    item.attendance_state = attendance.state.code;
                    item.department_name = attendance.department.code;

                    response.Add(item);
                }
                Log.Information(list.Count().ToString());
            }
            return response;
        }

        public string getToday()
        {
            string today = DateTime.Today.ToString("yyyy-01-dd");
            return today;
        }
        public bool getListNotiSignalR(string idHub)
        {
            DateTime now = DateTime.UtcNow;
            using (DataContext context = new DataContext())
            {
                SqlEmployee? user = context.employees.Where(s => s.IdHub.CompareTo(idHub) == 0).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                string today = getToday();
                List<Item_Attendance_Res>? list = Program.api_attendance.getList(getToday(), "all").OrderByDescending(s => s.time).ToList();
                try
                {
                    string data = JsonConvert.SerializeObject(list);
                    Program.notiHub?.Clients.Client(user.IdHub).SendCoreAsync("GetListNoti", new object[] { data });
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return false;
                }
            }

            return false;
        }


        public async Task tool_add_attendance()
        {
            using (DataContext context = new DataContext())
            {
                List<SqlEmployee> emps = context.employees
                    .Where(s => s.isDeleted == false)
                    .Include(s => s.attendances)
                    .Include(s=>s.department)
                    .ToList();
                List<SqlState> states = context.ATD_state.ToList();
                SqlState? ontime = states!.Where(s=>s.code.CompareTo("OnTime")==0).FirstOrDefault();
                SqlState? Absent = states!.Where(s=>s.code.CompareTo("Absent")==0).FirstOrDefault();
                SqlState? late = states!.Where(s=>s.code.CompareTo("Late")==0).FirstOrDefault();

                int today = DateTime.Today.Day;
                for (DateTime i = DateTime.Today.AddDays(-today+1 ); i <= DateTime.Today; i=i.AddDays(1))
                {
                    for(int j =0; j < emps.Count; j++)
                    {
                        if (!emps[j].attendances!.Where(s => s.time.Day == i.Day).Any()) // ko có điểm danh
                        {
                            SqlAttendance attendance = new SqlAttendance();
                            attendance.ID= DataContext.Generate_UID();
                            attendance.employee = emps[j];
                            attendance.department = emps[j].department;
                            if (attendance.ID % 3 == 0) // ontime
                            {
                                attendance.time = new DateTime(
                                    i.Year,
                                    i.Month,
                                    i.Day,
                                    7, 28, 0).ToUniversalTime().AddHours(7);
                                attendance.state = ontime;
                                context.attendances.Add(attendance);
                            }
                            else if (attendance.ID % 3 == 1) // late
                            {
                                attendance.time = new DateTime(
                                    i.Year,
                                    i.Month,
                                    i.Day,
                                    9, 12, 0).ToUniversalTime().AddHours(7);
                                attendance.state = late;
                                context.attendances.Add(attendance);
                            }
                            else // absent
                            {

                            }
                        }


                    }
                }
                int xz =await context.SaveChangesAsync();
                Log.Information(xz.ToString());
            }
        }
    }
}
