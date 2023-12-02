namespace se100_cs.APIs
{
    public class MyDashboard
    {
        public MyDashboard() { }

        public class Employees_Today
        {
            public int attendance { get; set; } = 0;
            public int late_coming { get; set; } = 0;
            public int absent { get; set; } = 0;

        }
        public class Total_Employee
        {
            public int man { get; set; } = 0;
            public int woman { get; set; } = 0;
        }
        public class Salary_byMonth
        {
            public long average { set; get; } = 0;
            public long min { set; get; } = 0;
            public long max { set; get; } = 0;
            public long total { set; get; } = 0;
        }

        protected class Response_DTO
        {
            public Employees_Today employees_Today { get; set; }= new Employees_Today();
            public Total_Employee total_Employee { get; set; } = new Total_Employee();
            public Salary_byMonth salary_ByMonth { get; set; } = new Salary_byMonth();

        }
        public string getStats()
        {
            Employees_Today employees_Today = new Employees_Today();
            Salary_byMonth salary_ByMonth = new Salary_byMonth();
            Total_Employee total_Employee   = new Total_Employee();

            Response_DTO response_DTO = new Response_DTO();
            response_DTO.employees_Today = employees_Today;
            response_DTO.total_Employee = total_Employee;
            response_DTO.salary_ByMonth = salary_ByMonth;
            string tmp = System.Text.Json.JsonSerializer.Serialize(response_DTO);

            return tmp;
        }
    }
}
