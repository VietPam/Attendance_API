using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Models;

public record CompanySettingModel(

    [FromRoute] int Id,
    string Name,
    string HourStartWorking,
    decimal SalaryPerCoef,
    int PaymentDate);
