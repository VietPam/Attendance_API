namespace se100_cs.Controllers.Attendance.ResponseDTO
{
    public class Attendance_RES
    {
        public string? employee_name { get; set; } = "";
        public DateTime time { get; set; } = new DateTime(1, 1, 1, 23, 59, 59);
        public string? attendance_state { get; set; } = "Absent";
        public string? department_name { get; set; } = "";
    }
}
