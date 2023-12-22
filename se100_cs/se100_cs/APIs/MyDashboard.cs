namespace se100_cs.APIs
{
    public class MyDashboard
    {
        public MyDashboard() { }
        public class Emp_perDep
        {
            public string department_name { get; set; }
            public int emp_count { get; set; }
        }
        protected class Response_DTO
        {
            public MyAttendance.Employees_Today employees_Today { get; set; }
            public MyEmployee.Total_Employee total_Employee { get; set; }
            public List<MyAttendance.Employees_Today> attendance_ByWeek { get; set; }
            public List<Emp_perDep> emp_perDepts { get; set; }

        }
        public string getStats()
        {
            MyAttendance.Employees_Today employees_Today = Program.api_attendance.getEmployees_Today();
            List<Emp_perDep> emp_perDepts = new List<Emp_perDep>();

            MyEmployee.Total_Employee total_Employee = Program.api_employee.getTotal_Employee();
            List<MyAttendance.Employees_Today> attendance_ByWeek = Program.api_attendance.getEmployees_ByWeek();
            Response_DTO response_DTO = new Response_DTO();
            response_DTO.employees_Today = employees_Today;
            response_DTO.total_Employee = total_Employee;
            response_DTO.emp_perDepts = emp_perDepts;
            response_DTO.attendance_ByWeek = attendance_ByWeek;
            string tmp = System.Text.Json.JsonSerializer.Serialize(response_DTO);

            return tmp;
        }
    }
}
