using Services.Services.Token;

namespace Services.Services.Auth;
public class AuthDTO { }
public class LoginDTO
{
    public int Id { get; set; }
    public string role { get; set; } = "";
    public TokenItem token { get; set; } = new TokenItem();
}
