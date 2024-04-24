using Microsoft.Extensions.DependencyInjection;
using Services.Services;
using Services.Services.Auth;
using Services.Services.Email;
using Services.Services.Token;

namespace Services;
public static class Extentions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<CompanySettingService>();
        services.AddScoped<RoleService>();
        services.AddScoped<AttenStateService>();
        services.AddScoped<DepartmentService>();
        services.AddScoped<AuthService>();
        services.AddScoped<TokenService>();
        services.AddScoped<EmailService>();
    }
}
