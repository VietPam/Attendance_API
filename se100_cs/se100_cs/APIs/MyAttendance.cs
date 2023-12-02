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
                List<SqlAttendance>? list = context.attendances!.Include(s => s.employee).Where(s => s.time.Day == day && s.time.Month == month && s.time.Year == year).ToList();

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
    }
}
