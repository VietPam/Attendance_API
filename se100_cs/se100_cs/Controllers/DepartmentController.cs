using Microsoft.AspNetCore.Mvc;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController: ControllerBase
    {
        [HttpGet]
        [Route("getAll")]
        public IActionResult getAll()
        {
            return Ok( Program.api_department.getAll());
        }

        public class Request_Department_DTO
        {
            public string name { get; set; } ="";
            public string code { get; set; } ="";
        }
        
        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew([FromBody] Request_Department_DTO dto)
        {
            bool tmp = await Program.api_department.createNew(dto.name, dto.code);
            if(tmp)
            {
                return Ok();
            }
            else return BadRequest();
        }
        [HttpPut]
        [Route("updateOne")]
        public async Task<IActionResult> updateOne([FromBody] Request_Department_DTO dto, long id)
        {
            bool tmp = await Program.api_department.updateOne(id, dto.name, dto.code);
            if( tmp)
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
        public async Task<IActionResult> deleteOne(string code)
        {
            bool tmp = await Program.api_department.deleteOne(code);
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
