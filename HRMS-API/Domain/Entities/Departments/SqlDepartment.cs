using Domain.Entities.Common;

namespace Domain.Entities.Departments;
public class SqlDepartment : Entity
{
    public string Name { get; set; } = "";
    public string Code { get; set; } = "";
    public string Description { get; set; } = "";
    public List<SqlUser>? Users { get; set; } = [];
    public List<SqlPosition>? Position { get; set; } = [];
}
