using Domain.Entities.Common;

namespace Domain.Entities.Accounts;
public class Role : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}