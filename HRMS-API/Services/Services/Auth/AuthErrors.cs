using Infrastructure.ResultExtension;

namespace Services.Services.Auth;
public class AuthErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error InvalidLogin => Error.NotFound(
        "Account.NotFound",
        $"Username or password is incorrect");
    public static Error NotFoundByEmail(string email) => Error.NotFound(
        "Users.NotFoundByEmail",
        $"The user with the Email = '{email}' was not found");

    public static Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
    public static Error RegisterFail => Error.Problem(
        "Users.RegisterError",
        $"Failed to register User");
    public static Error AccountYetVerified => Error.Problem(
        "Account.YetVerified",
        $"Account have not verified yet");
    public static Error VerifyFail => Error.Problem(
        "Users.VerifyError",
        $"Failed to Verify User");
    public static Error InvalidToken(string Token) => Error.NotFound(
       "Token.Invalid",
       $"The token = '{Token}' was invalid");
    public static Error PasswordConfirmMisMatch => Error.Conflict(
       "Password.Mismatch",
       $"The Confirm Password you entered does not match the Password");
}
