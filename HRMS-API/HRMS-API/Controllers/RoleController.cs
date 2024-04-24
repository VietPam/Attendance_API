using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace HRMS_API.Controllers;

public class RoleController(RoleService _roleService) : BaseController
{
    [HttpGet()]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _roleService.GetAllAsync());
    }
}
