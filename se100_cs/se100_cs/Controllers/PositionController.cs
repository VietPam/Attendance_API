using Microsoft.AspNetCore.Mvc;
using se100_cs.APIs;
using static se100_cs.Controllers.EmployeeController;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController: ControllerBase
    {
        public class Request_Position_DTO
        {
            public string title { get; set; } = "";
            public string code { get; set; } = "";
            public long salary_coeffcient { get; set; } = 0;

        }
        [HttpGet]
        [Route("getByDepartmentCode")]
        public IActionResult getByDepartmentCode(string departmentCode, int page=1, int per_page=10)
        {
            return Ok(Program.api_position.getByDepartmentCode(departmentCode,page,per_page));
        }

        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew([FromBody] Request_Position_DTO dto, string departmentCode)
        {
            bool tmp = await Program.api_position.createNew(dto.title, dto.code, dto.salary_coeffcient, departmentCode);
            if (tmp)
            {
                return Ok();
            }
            else return BadRequest();
        }
        //[HttpPut]
        //[Route("unlink-position")]
        //public async Task<IActionResult> remove_position(long userId)
        //{
        //    bool tmp = await Program.api_position.remove_position(userId);
        //    if (tmp)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        
        
        //[HttpPut]
        //[Route("updateOne")]
        //public async Task<IActionResult> updateOne([FromBody] Request_Position_DTO dto, long id)
        //{
        //    bool tmp = await Program.api_position.updateOne(id, dto.title, dto.code, dto.salary_coeffcient);
        //    if (tmp)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpDelete]
        //[Route("deleteOne")]
        //public async Task<IActionResult> deleteOne(long id)
        //{
        //    bool tmp = await Program.api_position.deleteOne(id);
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
