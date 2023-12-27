using se100_cs.Controllers.Dashboard.Response_DTO;

namespace se100_cs.APIs
{
    public class MyDashboard
    {
        public MyDashboard() { }
        public class Emp_perDep
        {
            public string department_name { get; set; } = "";
            public string department_code { get; set; } = "";
            public int emp_count { get; set; }
        }
        public class Response_DTO
        {
            public Dashboard_Attendance_RES employees_Today { get; set; }= new Dashboard_Attendance_RES();
            public MyEmployee.Total_Employee total_Employee { get; set; }= new MyEmployee.Total_Employee();
            public List<Dashboard_Attendance_RES> attendance_ByWeek { get; set; } = new List<Dashboard_Attendance_RES>();
            public List<Emp_perDep> emp_perDepts { get; set; } = new List<Emp_perDep>();

        }
        public Response_DTO getStats()
        {
            Response_DTO response = new Response_DTO();

            Dashboard_Attendance_RES employees_Today = Program.api_attendance.getEmployees_Today();
            response.employees_Today = employees_Today;

            List<Emp_perDep> emp_perDepts = Program.api_employee.GetEmp_PerDeps(5);

            response.emp_perDepts = emp_perDepts;

            MyEmployee.Total_Employee total_Employee = Program.api_employee.getTotal_Employee();
            response.total_Employee = total_Employee;


            List<Dashboard_Attendance_RES> attendance_ByWeek = Program.api_attendance.getEmployees_ByWeek();
            response.attendance_ByWeek = attendance_ByWeek;

            return response;
        }
    }
}
