using HRMS_API.Models;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace HRMS_API.Controllers;

public class CompanySettingController(CompanySettingService _companySettingService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCompanySettingAsync()
    {
        return Ok(await _companySettingService.GetAsync());
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateCompanySettingAsync(CompanySettingModel model)
    {
        DateTime HourStartWorking = DateTimeHelper.ParseDateTime(model.HourStartWorking) ?? DateTime.MinValue;

        bool IsSuccess = await _companySettingService.UpdateOne(model.Name, HourStartWorking, model.SalaryPerCoef, model.PaymentDate);

        return IsSuccess ? Ok() : BadRequest();
    }
}
