using Domain.Entities.Common;
using Domain.Entities.Departments;

namespace Domain.Entities.Accounts;
public class SqlAccount : Entity
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

    //credential verify
    public string? VerificationToken { get; set; }
    public DateTime? Verified { get; set; }


    //forgot password
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }

    public List<SqlToken> Tokens { get; set; } = new List<SqlToken>();
    public int? UserId { get; set; }
    public SqlUser? User { get; set; }
    public int RoleId { get; set; }
    public SqlRole Role { get; set; } = null!;


}