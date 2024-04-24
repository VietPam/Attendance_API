namespace Services.Services.Token;
public class TokenItem
{
    public string accessToken { get; set; } = "";
    public string refreshToken { get; set; } = "";
}

public class TokenDecodedInfo
{
    public int id { get; set; }
    public string role { get; set; } = "";
}