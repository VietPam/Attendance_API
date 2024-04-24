using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Mappers;

namespace Services.Services;
public class CompanySettingService(ApplicationDbContext context)
{
    public async Task InitAsync()
    {
        if (await context.CompanySettings.AnyAsync())
        {
            return;
        }

        SqlCompanySetting companySetting = new()
        {
            Name = "Initial Company",
            HourStartWorking = DateTime.UtcNow,
            SalaryPerCoef = 200000,
            PaymentDate = 15
        };
        await context.CompanySettings.AddAsync(companySetting);
        await context.SaveChangesAsync();
    }
    public async Task<CompanySettingDTO> GetAsync()
    {
        SqlCompanySetting? setting = await context.CompanySettings.FirstOrDefaultAsync();

        if (setting == null)
        {
            return new CompanySettingDTO();
        }

        return setting.ToDTO();
    }


    public async Task<bool> UpdateOne(string Name, // nhớ đổi chỗ code với name
                                        DateTime HourStartWorking,
                                        decimal SalaryPerCoef,
                                        int PaymentDate)
    {
        SqlCompanySetting? company = await context.CompanySettings.FirstOrDefaultAsync();
        if (company == null)
        {
            return false;
        }


        company.Name = Name;
        company.HourStartWorking = HourStartWorking;
        company.SalaryPerCoef = SalaryPerCoef;
        company.PaymentDate = PaymentDate;

        await context.SaveChangesAsync();
        return true;
    }
}
