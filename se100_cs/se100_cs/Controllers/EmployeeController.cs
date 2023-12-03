using Microsoft.AspNetCore.Mvc;
using se100_cs.APIs;
using se100_cs.Model;
using System.Runtime.CompilerServices;
using static se100_cs.Controllers.PositionController;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public class Request_Employee_DTO
        {
            public string email { get; set; } = "email la id";
            public string fullName { get; set; } = "";
            public string phoneNumber { get; set; } = "";
            public string avatar { get; set; } = "";
            public DateTime birth_day { get; set; } = DateTime.UtcNow;
            public bool gender { get; set; } = true;
            public string cmnd { get; set; } = "";
            public string address { get; set; } = "";
        }
        [HttpGet]
        [Route("getByDepartmentCode")]
        public IActionResult getByDepartmentCode(string departmentCode)
        {
            return Ok(Program.api_employee.getByDepartmentCode(departmentCode));
        }
        [HttpGet]
        [Route("getRole")]
        public IActionResult getRole(long id)
        {
            return Ok(Program.api_employee.getRole(id));
        }

        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew([FromBody] Request_Employee_DTO dto, string departmentCode)
        {
            bool tmp = await Program.api_employee.createNew(dto.email, dto.fullName, dto.phoneNumber, dto.birth_day, dto.gender, dto.cmnd, dto.address, dto.avatar, departmentCode);
            if (tmp)
            {
                return Ok();
            }
            else return BadRequest();
        }
        public class login_dto_request
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] login_dto_request dto)
        {
            string token = Program.api_employee.login(dto.email, dto.password);
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }
            else
            {
                return Ok(token);
            }
        }

        [HttpPut]
        [Route("updateOne")]
        public async Task<IActionResult> updateOne([FromBody] Request_Employee_DTO dto, long id)
        {
            bool tmp = await Program.api_employee.updateOne(dto.email, dto.fullName, dto.phoneNumber, dto.birth_day, dto.gender, dto.cmnd, dto.address, dto.avatar, id);
            if (tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("updateRole")]
        public async Task<IActionResult> updateRole(string role, long id)
        {
            bool tmp = await Program.api_employee.updateRole(role, id);
            if (tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("deleteOne")]
        public async Task<IActionResult> deleteOne(long id)
        {
            bool tmp = await Program.api_employee.deleteOne(id);
            if (tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
