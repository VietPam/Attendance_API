using HRMS_API.Models;
using Infrastructure;
using Infrastructure.ResultExtension;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Auth;
using Services.Services.Token;

namespace HRMS_API.Controllers;

public class AuthController(AuthService _authService) : BaseController
{
    [HttpPost("login")]
    public async Task<IResult> loginWTokenAsync(LoginModel model)
    {
        Result<LoginDTO> result = await _authService.LoginAsync(model.Email, model.Password);

        if (result.IsFailure) return CustomResults.Problem(result);

        LoginDTO user = result.Value;

        AddCookiesToResponse(user.token);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    [HttpPost("register")]
    public async Task<IResult> register(RegisterModel model)
    {
        string? currentRole = HttpContext.Items["role"] as string;
        string role = "User";
        if (currentRole == "Admin") role = model.Role;

        Result<int> result = await _authService.RegisterAsync(model.Email, model.Password, Request.Headers["origin"], role);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    #region helper function
    private void AddCookiesToResponse(TokenItem token)
    {
        // Thêm cookies vào Response
        Response.Cookies.Append(ConfigKey.AT_COOKIES, token.accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Yêu cầu kết nối bảo mật (HTTPS)
            SameSite = SameSiteMode.None, // Để chấp nhận các yêu cầu từ các trang web khác
            Expires = ConfigKey.getATExpiredTime() // Thời gian sống của cookie
        });

        Response.Cookies.Append(ConfigKey.RT_COOKIES, token.refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = ConfigKey.getRTExpiredTime()
        });
    }
    #endregion
}
