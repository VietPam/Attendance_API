using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using System.Net;

namespace HRMS_API.Middlewares;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }

    public static IApplicationBuilder UseTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenMiddleware>();
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    Debug.WriteLine($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync("Error");
                }
            });
        });
    }
}
