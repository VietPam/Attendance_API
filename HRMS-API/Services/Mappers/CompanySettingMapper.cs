using Domain.Entities;
using Services.DTOs;

namespace Services.Mappers;
public static class CompanySettingMapper
{
    public static CompanySettingDTO ToDTO(this CompanySetting entity)
    {
        return new CompanySettingDTO(entity.Name,
                                    entity.Code,
                                    entity.HourStartWorking,
                                    entity.SalaryPerCoef,
                                    entity.PaymentDate);
    }

}
