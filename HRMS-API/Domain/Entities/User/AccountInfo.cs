using Domain.Entities.Common;

namespace Domain.Entities.User;
public class AccountInfo : Entity
{
    public required string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Avatar { get; set; }
    public DateTime? BirthDay { get; set; }
    public bool? Gender { get; set; }
    public string? IdNumber { get; set; }
    public string? Address { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
}
