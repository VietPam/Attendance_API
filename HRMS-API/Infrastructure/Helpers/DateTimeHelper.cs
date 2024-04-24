namespace Infrastructure.Helpers;
public static class DateTimeHelper
{
    public const string format = "yyyy-MM-dd HH:mm:ss";
    public static DateTime? ParseDateTime(string dateTimeString, string format = format)
    {
        DateTime parsedDateTime;

        if (DateTime.TryParseExact(dateTimeString, format,
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out parsedDateTime))
        {
            return parsedDateTime;
        }
        else
        {
            // Parsing failed, return null or handle the error as needed
            return null;
        }
    }
}