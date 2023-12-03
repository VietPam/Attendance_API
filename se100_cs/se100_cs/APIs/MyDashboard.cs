namespace se100_cs.APIs
{
    public class MyDashboard
    {
        public MyDashboard() { }
        public class Salary_byMonth
        {
            public long average { set; get; } = 0;
            public long min { set; get; } = 0;
            public long max { set; get; } = 0;
            public long total { set; get; } = 0;
        }
        //public class Attendance_byWeek
        //{
        //    public MyAttendance.Employees_Today monday = new MyAttendance.Employees_Today();
        //    public MyAttendance.Employees_Today tuesday = new MyAttendance.Employees_Today();
        //    public MyAttendance.Employees_Today wednesday = new MyAttendance.Employees_Today();
        //    public MyAttendance.Employees_Today thurday= new MyAttendance.Employees_Today();
        //    public MyAttendance.Employees_Today friday= new MyAttendance.Employees_Today();
        //    public MyAttendance.Employees_Today saturday= new MyAttendance.Employees_Today();
        //    public MyAttendance.Employees_Today sunday= new MyAttendance.Employees_Today();
        //}
        protected class Response_DTO
        {
            public MyAttendance.Employees_Today employees_Today { get; set; }
            public MyEmployee.Total_Employee total_Employee { get; set; }
            public MyAttendance.Attendance_byWeek attendance_ByWeek { get; set; }
            public Salary_byMonth salary_ByMonth { get; set; } = new Salary_byMonth();

        }
        public string getStats()
        {
            MyAttendance.Employees_Today employees_Today = Program.api_attendance.getEmployees_Today();
            Salary_byMonth salary_ByMonth = new Salary_byMonth();
            MyEmployee.Total_Employee total_Employee = Program.api_employee.getTotal_Employee();
            MyAttendance.Attendance_byWeek attendance_ByWeek = Program.api_attendance.getEmployees_ByWeek();
            Response_DTO response_DTO = new Response_DTO();
            response_DTO.employees_Today = employees_Today;
            response_DTO.total_Employee = total_Employee;
            response_DTO.salary_ByMonth = salary_ByMonth;
            response_DTO.attendance_ByWeek = attendance_ByWeek;
            string tmp = System.Text.Json.JsonSerializer.Serialize(response_DTO);

            return tmp;
        }
    }
}
