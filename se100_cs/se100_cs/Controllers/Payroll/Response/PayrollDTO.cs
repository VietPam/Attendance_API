namespace se100_cs.Controllers.Payroll.Response
{
    public class PayrollDTO
    {
        public class Payroll_DTO_Res
        {
            public long Employee_ID { get; set; }
            public string Avatar { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public double Coefficient { get; set; }
            public int day_of_work { get; set; }
            public double bonus_penalty { get; set; } = 0;
            public double salary { get; set; }
        }
    }
}
