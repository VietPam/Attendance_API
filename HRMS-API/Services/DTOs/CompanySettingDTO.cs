namespace Services.DTOs;
public class CompanySettingDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HourStartWorking { get; set; }
    public decimal SalaryPerCoef { get; set; }
    public int PaymentDate { get; set; }

    public CompanySettingDTO()
    {
        Id = -1;
        Name = "";
        HourStartWorking = "";
        SalaryPerCoef = 0;
        PaymentDate = 0;
    }

    public CompanySettingDTO(int id, string name, string hourStartWorking, decimal salaryPerCoef, int paymentDate)
    {
        Id = id;
        Name = name;
        HourStartWorking = hourStartWorking;
        SalaryPerCoef = salaryPerCoef;
        PaymentDate = paymentDate;
    }
}
