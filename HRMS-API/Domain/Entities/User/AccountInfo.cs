using Domain.Entities.Common;

namespace Domain.Entities.User;
public class AccountInfo : Entity
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public DateTime birthDay { get; set; } = DateTime.UtcNow;
    public bool Gender { get; set; } = true;
    public string IdNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
