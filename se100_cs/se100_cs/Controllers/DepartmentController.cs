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

        public class Request_Department_DTO_New
        {
            public string name { get; set; } ="";
            public string code { get; set; } ="";
        }
        public class Request_Department_DTO_Update
        {
            public long ID { get; set; } 
            public string name { get; set; } = "";
            public string code { get; set; } = "";
        }

        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> createNew([FromBody] Request_Department_DTO_New dto)
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
        public async Task<IActionResult> updateOne([FromBody]Request_Department_DTO_Update dto)
        {
            bool tmp = await Program.api_department.updateOne(dto.ID, dto.name, dto.code);
            if( tmp)
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
