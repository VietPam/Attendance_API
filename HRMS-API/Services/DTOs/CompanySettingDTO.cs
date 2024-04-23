namespace Services.DTOs;
public class CompanySettingDTO
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DateTime? HourStartWorking { get; set; }
    public decimal SalaryPerCoef { get; set; }
    public int PaymentDate { get; set; }

    public CompanySettingDTO()
    {
        Name = "";
        Code = "";
        HourStartWorking = null;
        SalaryPerCoef = 0;
        PaymentDate = 0;
    }

    public CompanySettingDTO(string name, string code, DateTime? hourStartWorking, decimal salaryPerCoef, int paymentDate)
    {
        Name = name;
        Code = code;
        HourStartWorking = hourStartWorking;
        SalaryPerCoef = salaryPerCoef;
        PaymentDate = paymentDate;
    }
}
