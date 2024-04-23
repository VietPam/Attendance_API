using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Mappers;

namespace Services.APIs;
public class CompanySettingService(ApplicationDbContext context)
{
    public CompanySettingDTO Get()
    {
        CompanySetting? setting = context.CompanySettings.FirstOrDefault();

        if (setting == null)
        {
            return new CompanySettingDTO();
        }

        return setting.ToDTO();
    }


    public async Task<bool> UpdateOne(string Name, // nhớ đổi chỗ code với name
                                        string Code,
                                        DateTime HourStartWorking,
                                        decimal SalaryPerCoef,
                                        int PaymentDate)
    {
        CompanySetting? company = await context.CompanySettings.FirstOrDefaultAsync() ?? new CompanySetting();

        company.Name = Name;
        company.Code = Code;
        company.HourStartWorking = HourStartWorking;
        company.SalaryPerCoef = SalaryPerCoef;
        company.PaymentDate = PaymentDate;

        await context.SaveChangesAsync();
        return true;
    }
}
