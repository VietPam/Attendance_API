using Domain.Entities.Common;

namespace Domain.Entities.User;
public class Account : Entity
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    //public Role Role { get; set; }
    public AccountInfo? AccountInfo { get; set; }
}

//public Role? role { get; set; } = Role.EMPLOYEE;

//public enum Role
//{
//    ADMIN,
//    DIRECTOR,
//    MANAGER,
//    EMPLOYEE
//}