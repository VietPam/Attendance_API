using Microsoft.AspNetCore.Mvc;
using se100_cs.APIs;
using se100_cs.Model;
using static se100_cs.Controllers.PositionController;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        public class Request_Employee_DTO
        {
            public string email { get; set; }
        }
        [HttpGet]
        [Route("getByDepartmentCode")]
        public IActionResult getByDepartmentCode(string departmentCode)
        {
            return Ok(Program.api_employee.getByDepartmentCode(departmentCode));
        }

        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew([FromBody] Request_Employee_DTO dto, string departmentCode)
        {
            bool tmp = await Program.api_employee.createNew(dto.email, departmentCode);
            if (tmp)
            {
                return Ok();
            }
            else return BadRequest();
        }
    }
}
