using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using Serilog;

namespace se100_cs.APIs
{
    public class MyAttendance
    {
        public MyAttendance() { }
        public class Attendance_DTO_Response
        {
            public DateTime time { get; set; } = DateTime.UtcNow;
            public string status { get; set; } = "Absent";
            public string name { get; set; }
        }


        public List<Attendance_DTO_Response> getListByDate(int day, int month, int year)
        {
            List<Attendance_DTO_Response> response = new List<Attendance_DTO_Response>();
            using (DataContext context = new DataContext())
            {
                List<SqlAttendance>? list = context.attendances!.Include(s => s.employee).Where(s => s.employee.isDeleted == false && s.time.Day == day && s.time.Month == month && s.time.Year == year).ToList();

                if (list.Count > 0)
                {
                    foreach (SqlAttendance attendance in list)
                    {
                        Attendance_DTO_Response item = new Attendance_DTO_Response();
                        item.time = attendance.time;
                        if (attendance.status == 0) { item.status = "On Time"; }
                        else if (attendance.status == 1) { item.status = "Late"; }
                        item.name = attendance.employee.fullName;
                        response.Add(item);
                    }
                }
            }
            return response;
        }

        public string attendance_status(int i)
        {
            if (i == 0) return "OnTime";
            if (i == 1) return "Late";
            return "Absent";
        }
        public async Task<string> markAttendance(long id)
            {
            string status;
            DateTime now = DateTime.UtcNow;
            int today = now.Day;
            SqlSetting? setting = new SqlSetting();
            using (DataContext context = new DataContext())
            {
                try
                {
                    setting = context.settings!.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return "Setting null";
                }
                int start_time_hour = setting.start_time_hour;
                int start_time_minute = setting.start_time_minute;

                SqlEmployee employee = context.employees!.Where(s=>s.ID == id).FirstOrDefault();
                if (employee == null)
                {
                    return "employee null";
                }
                SqlAttendance existing = context.attendances!.Include(s=>s.employee).Where(s=>s.employee.ID== id &&s.time.Day==today).FirstOrDefault();

                if (existing != null)
                {
                    status = attendance_status(existing.status);
                    return status ;
                }
                SqlAttendance sqlAttendance = new SqlAttendance();
                sqlAttendance.employee = employee;
                sqlAttendance.time = now;
                if(now.Hour*60+now.Minute>start_time_hour*60+start_time_minute)
                {
                    sqlAttendance.status = 1;
                }
                else
                {
                    sqlAttendance.status = 0;
                }
                context.attendances!.Add(sqlAttendance);
                status = attendance_status(sqlAttendance.status);
                try
                {
                int rows = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Log.Information(ex.ToString());
                }
            }

            return status;
        }
        public class Employees_Today
        {
            public int attendance { get; set; } = 0;
            public int late_coming { get; set; } = 0;
            public int absent { get; set; } = 0;

        }

        public List<Employees_Today> getEmployees_ByWeek()
        {
            int ngay_hom_nay = DateTime.Now.Day;
            List<Employees_Today> week = new List<Employees_Today>();
            int thumay = (int)DateTime.Now.DayOfWeek; // vi du thu 5
            for(int i =thumay; i>=2 ; i--)
            {
                Employees_Today today = new Employees_Today();
                using (DataContext context = new DataContext())
                {
                    int attendance = context.attendances.Where(s => s.status == 0 && s.time.Day == ngay_hom_nay-(thumay-i)).Count();
                    int late_coming = context.attendances!.Where(s => s.status == 1 && s.time.Day == ngay_hom_nay - (thumay - i)).Count();
                    int total = Program.api_employee.countTotalEmployee();
                    int absent = total - attendance - late_coming;
                    today.attendance = attendance;
                    today.absent = absent;
                    today.late_coming = late_coming;
                    week.Add(today);
                }

            }    
            using (DataContext context = new DataContext())
            {
                int attendance = context.attendances.Where(s => s.status == 0 && s.time.Day == day).Count();
                int late_coming = context.attendances!.Where(s => s.status == 1 && s.time.Day == day).Count();
                int total = Program.api_employee.countTotalEmployee();
                int absent = total - attendance - late_coming;
                today.attendance = attendance;
                today.absent = absent;
                today.late_coming = late_coming;
            }
            return week;
        }
        public Employees_Today getEmployees_Byday(int day) 
        {
            Employees_Today today = new Employees_Today();
            int count_attendance = 0;
            using (DataContext context = new DataContext())
            {
                int attendance = context.attendances.Where(s => s.status == 0 && s.time.Day == day).Count();
                int late_coming = context.attendances!.Where(s => s.status == 1 && s.time.Day == day).Count();
                int total = Program.api_employee.countTotalEmployee();
                int absent = total - attendance - late_coming;
                today.attendance = attendance;
                today.absent = absent;
                today.late_coming = late_coming;
            }
            return today;
        }
        public Employees_Today getEmployees_Today()
        {
            int day = DateTime.Now.Day;
            Employees_Today today = new Employees_Today();
            int count_attendance=0;
            using(DataContext context = new DataContext())
            {
                int attendance = context.attendances.Where(s=>s.status==0 && s.time.Day==day).Count();
                int late_coming = context.attendances!.Where(s=>s.status==1 && s.time.Day == day).Count();
                int total = Program.api_employee.countTotalEmployee();
                int absent = total - attendance - late_coming;
                today.attendance = attendance;
                today.absent = absent;
                today.late_coming= late_coming;
            }
            return today;
        }
    }
}
