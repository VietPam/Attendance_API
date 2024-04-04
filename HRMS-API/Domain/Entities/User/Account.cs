using Domain.Entities.Common;

namespace Domain.Entities.User;
public class Account : Entity
{
    public string Email { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public Role? role { get; set; } = Role.EMPLOYEE;

}
public enum Role
{
    ADMIN,
    DIRECTOR,
    MANAGER,
    EMPLOYEE
}