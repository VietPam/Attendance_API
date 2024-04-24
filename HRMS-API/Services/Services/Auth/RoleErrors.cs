using Infrastructure.ResultExtension;

namespace Services.Services.Auth;
public class RoleErrors
{
    public static Error NotFound(string roleCode) => Error.NotFound(
    "Roles.NotFound",
    $"The role with the Code = '{roleCode}' was not found");
}
