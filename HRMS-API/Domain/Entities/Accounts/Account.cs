using Domain.Entities.Common;
using Domain.Entities.Departments;

namespace Domain.Entities.Accounts;
public class Account : Entity
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public User? User { get; set; }
    public Role Role { get; set; } = null!;
}