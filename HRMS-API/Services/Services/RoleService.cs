using Domain.Entities.Accounts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Mappers;

namespace Services.Services;
public class RoleService(ApplicationDbContext context)
{
    public async Task InitAsync()
    {
        bool NeedToSave = false;
        List<string> stateCodes = ["Employee", "Admin"];

        List<Role> roles = await context.Roles.Where(s => stateCodes.Contains(s.Code)).ToListAsync();

        foreach (string stateCode in stateCodes)
        {
            if (!roles.Any(s => s.Code == stateCode))
            {
                context.Roles.Add(new Role { Code = stateCode, Name = stateCode, Description = stateCode });
                NeedToSave = true;
            }
        }

        if (NeedToSave)
        {
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<RoleDTO>> GetAllAsync()
    {
        List<Role> roles = await context.Roles.ToListAsync();

        return roles.Select(s => s.ToDTO()).ToList();
    }
}
