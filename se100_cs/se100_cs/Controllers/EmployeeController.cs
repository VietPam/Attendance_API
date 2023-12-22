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
        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew(Request_Employee_DTO _DTO, string department_code)
        {
            bool tmp = await Program.api_employee.createNew(_DTO.email, _DTO.fullName, _DTO.phoneNumber, _DTO.birth_day, _DTO.gender, _DTO.cmnd, _DTO.address, _DTO.avatar, department_code);
            if(tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        public class Non_Position_Emp
        {
            public long user_ID { get; set; }
            public string fullName { get; set; } = "";
            public string avatar { get; set; } = "";
            public bool gender { get; set; } = true;
            public string department_code { get; set; } = "";
        }
        

        
        //[HttpGet]
        //[Route("list-non-position")]
        //public IActionResult list_non_position()
        //{
        //    return Ok (Program.api_employee.list_non_position());
            
        //}

        [HttpGet]
        [Route("getByDepartmentCode")]
        public IActionResult getByDepartmentCode(string departmentCode,int page=1, int per_page=10)
        {
            return Ok(Program.api_employee.getByDepartmentCode(departmentCode,page,per_page));
        }

        [HttpGet]
        [Route("getByPositionID")]
        public IActionResult getByPositionID(long positionId, int page=1, int per_page=10)
        {
            return Ok(Program.api_employee.getByPositionID(positionId,page,per_page));
        }

        public class login_dto_request
        {
            public string email { get; set; }
            public string password { get; set; }
        }
        //[HttpPut]
        //[Route("link-to-position")]
        //public async Task<IActionResult> link_to_position(long userId, long position_id)
        //{
        //    bool tmp = await Program.api_employee.link_to_position(userId, position_id);
        //    if (tmp)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] login_dto_request dto)
        {
                return Ok(Program.api_employee.login(dto.email, dto.password));
        }

        //[HttpPost]
        //[Route("reset-password")]
        //public IActionResult send_mail_reset_password() {
            
        //}

        //[HttpPut]
        //[Route("updateOne")]
        //public async Task<IActionResult> updateOne([FromBody] Request_Employee_DTO dto, long id)
        //{
        //    bool tmp = await Program.api_employee.updateOne(dto.email, dto.fullName, dto.phoneNumber, dto.birth_day, dto.gender, dto.cmnd, dto.address, dto.avatar, id);
        //    if (tmp)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        ////[HttpPut]
        ////[Route("updateRole")]
        ////public async Task<IActionResult> updateRole(string role, long id)
        ////{
        ////    bool tmp = await Program.api_employee.updateRole(role, id);
        ////    if (tmp)
        ////    {
        ////        return Ok();
        ////    }
        ////    else
        ////    {
        ////        return BadRequest();
        ////    }
        ////}
        //[HttpDelete]
        //[Route("deleteOne")]
        //public async Task<IActionResult> deleteOne(long id)
        //{
        //    bool tmp = await Program.api_employee.deleteOne(id);
        //    if (tmp)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
