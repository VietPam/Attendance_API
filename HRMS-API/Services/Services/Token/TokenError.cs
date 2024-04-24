using Infrastructure.ResultExtension;

namespace Services.Services.Token;
public class TokenError
{
    public static readonly Error TokenNotUnique = Error.Conflict(
          "Token.NotUnique",
          "The provided token is not unique.");

    public static readonly Error DatabaseSaveFailed = Error.Failure(
        "Token.SaveFailed",
        "Failed to save token to database.");
    public static Error NotFound(int userId) => Error.NotFound(
        "Token.NotFound",
        $"The user with the Id = '{userId}' was not found");
    public static Error CreateFail => Error.Problem(
        "Token.CreationError",
        $"Failed to create token");
    public static Error GenerateFail => Error.Problem(
        "Token.GenerationError",
        $"Failed to generate token");
}