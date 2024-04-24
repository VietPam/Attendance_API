using Domain.Entities.Common;

namespace Domain.Entities.Accounts;
public class SqlToken : Entity
{
    public string accessToken { get; set; } = string.Empty;

    public string refreshToken { get; set; } = string.Empty;

    public bool isExpired { get; set; } = false;

    public SqlAccount? Account { get; set; }

    public DateTime createTime { get; set; } = DateTime.UtcNow;

    public DateTime expiredTime { get; set; }

}
