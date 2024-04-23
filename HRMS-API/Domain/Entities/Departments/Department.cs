using Domain.Entities.Common;

namespace Domain.Entities.Departments;
public class Department : Entity
{
    public string Name { get; set; } = "";
    public string Code { get; set; } = "";
    public List<User>? Users { get; set; } = [];
    public List<Position>? Position { get; set; } = [];
}
