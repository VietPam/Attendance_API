namespace Services;
public class AppSettings
{
    public static string ConnectionStrings { get; private set; }
    public static string Secret { get; set; }
    public static string CORS { get; set; }

    // email
    public static string Mail { get; set; } = "finalproject7979@gmail.com";
    public static string DisplayName { get; set; } = "Final Project";
    public static string Password { get; set; } = "abmgyncswouicrkm";
    public static string Host { get; set; } = "smtp.gmail.com";
    public static int Port { get; set; } = 587;

    // google login 
    public static string GoogleClientId { get; set; }

    public static string GoogleClientSecret { get; set; }
}