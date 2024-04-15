using Domain.Entities.Common;

namespace Domain.Entities.User;
public class Role : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
