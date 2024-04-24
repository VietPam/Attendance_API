using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class ConfigKey
{
    private readonly IConfiguration _configuration;

    public static string AT_COOKIES { get; set; } = "ES_access_token";
    public static string RT_COOKIES { get; set; } = "ES_refresh_token";
    public static string JWT_KEY { get; set; } = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcwOTQ1NjQwMSwiaWF0IjoxNzA5NDU2NDAxfQ.uEeXhOemUcL_XOuDJl2AtdfjXphibM_2KC3hQ77ss4IeyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcwOTQ1NjQwMSwiaWF0IjoxNzA5NDU2NDAxfQ.uEeXhOemUcL_XOuDJl2AtdfjXphibM_2KC3hQ77ss4I";
    public static string VALID_AUDIENCE { get; set; } = "User";
    public static string VALID_ISSUER { get; set; } = "https://localhost:7032";
    public static string CLOUD_NAME { get; set; } = "";
    public static string CLOUD_APIKEY { get; set; } = "";
    public static string CLOUD_APISECRET { get; set; } = "";
    public static DateTime getATExpiredTime() => DateTime.UtcNow.AddMinutes(60);

    public static DateTime getRTExpiredTime() => DateTime.UtcNow.AddHours(1);

    public ConfigKey(IConfiguration configuration)
    {
        _configuration = configuration;
        AT_COOKIES = _configuration["JWT:AccessTokenCookies"];
        RT_COOKIES = _configuration["JWT:RefreshTokenCookies"];
        JWT_KEY = _configuration["JWT:Key"];
        VALID_AUDIENCE = _configuration["JWT:ValidAudience"];
        VALID_ISSUER = _configuration["JWT:ValidIssuer"];
        CLOUD_NAME = _configuration["Cloudinary:CloudName"];
        CLOUD_APIKEY = _configuration["Cloudinary:ApiKey"];
        CLOUD_APISECRET = _configuration["Cloudinary:ApiSecret"];
    }
}
