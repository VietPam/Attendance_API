using Domain.Entities;
using Services.DTOs;

namespace Services.Mappers;
public static class CompanySettingMapper
{
    public static CompanySettingDTO ToDTO(this SqlCompanySetting entity)
    {
        return new CompanySettingDTO(
            entity.Id,
            entity.Name,
            entity.HourStartWorking.ToString("HH:mm:ss yyyy-MM-dd"),
            entity.SalaryPerCoef,
            entity.PaymentDate);
    }

}
