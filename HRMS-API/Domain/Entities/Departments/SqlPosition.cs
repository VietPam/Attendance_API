using Domain.Entities.Common;

namespace Domain.Entities.Departments;
public class SqlPosition : Entity
{
    public string Title { get; set; } = "";
    public string Code { get; set; } = "";
    public decimal SalaryCoeffcient { get; set; } = 1;
    public SqlDepartment Department { get; set; } = null!;
    public List<SqlUser> Users { get; set; } = [];
}