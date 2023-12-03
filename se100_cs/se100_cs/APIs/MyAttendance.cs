using Microsoft.EntityFrameworkCore;
using se100_cs.Model;

namespace se100_cs.APIs
{
    public class MyAttendance
    {
        public MyAttendance() { }
        public class Attendance_DTO_Response
        {
            public DateTime time { get; set; } = DateTime.Now;
            public attendance_status status { get; set; } = attendance_status.Absent;
            public string name { get; set; }
        }


        public List<Attendance_DTO_Response> getListByDate(int day, int month, int year)
        {
            List<Attendance_DTO_Response> response = new List<Attendance_DTO_Response>();
            using (DataContext context = new DataContext())
            {
                List<SqlAttendance>? list = context.attendances!.Include(s => s.employee).Where(s =>s.employee.isDeleted==false&& s.time.Day == day && s.time.Month == month && s.time.Year == year).ToList();

                if (list.Count > 0)
                {
                    foreach (SqlAttendance attendance in list)
                    {
                        Attendance_DTO_Response item = new Attendance_DTO_Response();
                        item.time = attendance.time;
                        item.status = attendance.status;
                        item.name = attendance.employee.fullName;
                        response.Add(item);
                    }
                }
            }
            return response;
        }

        public async Task<bool> createNew(long emp_id, DateTime time)
        {
            DateTime now = DateTime.Now;
            using(DataContext context = new DataContext())
            {
                SqlSetting setting = context.settings!.FirstOrDefault();
                //int hour
                
                //SqlEmployee? employee= context.employees!.Where(s=>s.isDeleted==false && s.ID==emp_id).FirstOrDefault();
                //if (employee==null)
                //{
                //    return false;
                //}    

                //SqlAttendance? existing = context.attendances!.Include(s => s.employee).Where(s => s.employee.ID == emp_id).FirstOrDefault();
                //if (existing != null)
                //{
                //    return false;
                //}
                //SqlAttendance attendance= new SqlAttendance();
                //attendance.employee = employee;
                //attendance.time = now;
                //if (now.Hour<)
            }
            return false;
        }
    }
}
