using Domain.Entities.Common;

namespace Domain.Entities;
public class CompanySetting : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? HourStartWorking { get; set; }
    public decimal SalaryPerCoef { get; set; } = 200000;
    public int PaymentDate { get; set; } = 15;
}
