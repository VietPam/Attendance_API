using Domain.Entities.Common;

namespace Domain.Entities;
public class SqlCompanySetting : Entity
{
    public string Name { get; set; } = string.Empty;
    public DateTime HourStartWorking { get; set; } = DateTime.UtcNow;
    public decimal SalaryPerCoef { get; set; } = 200000;
    public int PaymentDate { get; set; } = 15;
}
