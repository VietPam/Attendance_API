using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Services;
namespace Services;
public static class SeedDatabase
{
    public static async Task SeedAsync(this IServiceProvider services)
    {
        try
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!await context.Roles.AnyAsync())
                {
                    RoleService roleService = scope.ServiceProvider.GetRequiredService<RoleService>();
                    await roleService.InitAsync();
                }

                if (!await context.AttenStates.AnyAsync())
                {
                    AttenStateService attenStateService = scope.ServiceProvider.GetRequiredService<AttenStateService>();
                    await attenStateService.InitAsync();
                }

                if (!await context.CompanySettings.AnyAsync())
                {
                    CompanySettingService companySettingService = scope.ServiceProvider.GetRequiredService<CompanySettingService>();
                    await companySettingService.InitAsync();
                }

                if (!await context.Departments.AnyAsync())
                {
                    DepartmentService departmentService = scope.ServiceProvider.GetRequiredService<DepartmentService>();
                    await departmentService.InitAsync();
                }

                //if (!await context.Accounts.AnyAsync())
                //{
                //    AuthService authService = scope.ServiceProvider.GetRequiredService<AuthService>();
                //    await authService.InitAsync();
                //}
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
