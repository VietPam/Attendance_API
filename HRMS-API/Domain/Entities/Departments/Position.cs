using Domain.Entities.Common;

namespace Domain.Entities.Departments;
public class Position : Entity
{
    public string Title { get; set; } = "";
    public string Code { get; set; } = "";
    public decimal SalaryCoeffcient { get; set; } = 1;
    public Department Department { get; set; } = null!;
    public List<User> Users { get; set; } = [];
}