using Microsoft.AspNetCore.Mvc;
using se100_cs.APIs;

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
        public IActionResult getByDepartmentCode(string departmentCode)
        {
            List<MyPosition.Position_DTO_Response> response = Program.api_position.getByDepartmentCode(departmentCode);
            if(response.Count > 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew([FromBody] Request_Position_DTO dto, string departmentCode)
        {
            bool tmp = await Program.api_position.createNew(dto.title, dto.code,dto.salary_coeffcient,departmentCode);
            if (tmp)
            {
                return Ok();
            }
            else return BadRequest();
        }
        [HttpPut]
        [Route("updateOne")]
        public async Task<IActionResult> updateOne([FromBody] Request_Position_DTO dto, long id)
        {
            bool tmp = await Program.api_position.updateOne(id, dto.title, dto.code, dto.salary_coeffcient);
            if (tmp)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpDelete]
        //[Route("deleteOne")]
        //public async Task<IActionResult> deleteOne([FromBody] Request_Department_DTO_Delete dto)
        //{
        //    bool tmp = await Program.api_position.deleteOne(dto.code);
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
