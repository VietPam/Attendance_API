namespace se100_cs.APIs
{
    public class MyDashboard
    {
        public MyDashboard() { }
        public class Emp_perDep
        {
            public string department_name { get; set; }
            public string department_code { get; set; }
            public int emp_count { get; set; }
        }
        protected class Response_DTO
        {
            public MyAttendance.Employees_Today employees_Today { get; set; }
            public MyEmployee.Total_Employee total_Employee { get; set; }
            public List<MyAttendance.Employees_Today> attendance_ByWeek { get; set; }
            public List<Emp_perDep> emp_perDepts { get; set; }

        }
        public async Task<string> getStats()
        {
            Response_DTO response = new Response_DTO();

            MyAttendance.Employees_Today employees_Today = Program.api_attendance.getEmployees_Today();
            response.employees_Today = employees_Today;

            List<Emp_perDep> emp_perDepts = Program.api_employee.GetEmp_PerDeps(5);

            response.emp_perDepts = emp_perDepts;

            MyEmployee.Total_Employee total_Employee = Program.api_employee.getTotal_Employee();
            response.total_Employee = total_Employee;


            List<MyAttendance.Employees_Today> attendance_ByWeek =await Program.api_attendance.getEmployees_ByWeek();
            response.attendance_ByWeek = attendance_ByWeek;

            string tmp = System.Text.Json.JsonSerializer.Serialize(response);

            return tmp;
        }
    }
}
