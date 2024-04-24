using Services.Services.Token;

namespace HRMS_API.Middlewares;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        TokenService? service = context.RequestServices.GetService<TokenService>();
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            TokenDecodedInfo info = service.DecodeToken(token);
            if (!string.IsNullOrEmpty(info.role))
            {
                context.Items["id"] = info.id;
                context.Items["role"] = info.role;
                //context.Items["token"] = token;
            }
        }
        await _next(context);
    }
}
