using Infrastructure.ResultExtension;

namespace Services.Services.Email;
public class AuthErrors
{
    public static Error Fail => Error.Problem(
        "EmailSending.Failure",
        $"The system can not send an email due to a system API error.");
}
