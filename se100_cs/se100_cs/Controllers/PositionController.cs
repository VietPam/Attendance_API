using Microsoft.AspNetCore.Mvc;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController: ControllerBase
    {
        
        [HttpGet]
        [Route("getByDepartmentCode")]
        public IActionResult getByDepartmentCode(string departmentCode)
        {
            return Ok(Program.api_position.getByDepartmentCode(departmentCode));
        }
        
        //[HttpPost]
        //[Route("createNew")]
        //public async Task<IActionResult> createNew([FromBody] Request_Department_DTO_New dto)
        //{
        //    bool tmp = await Program.api_position.createNew(dto.name, dto.code);
        //    if (tmp)
        //    {
        //        return Ok();
        //    }
        //    else return BadRequest();
        //}
        //[HttpPut]
        //[Route("updateOne")]
        //public async Task<IActionResult> updateOne([FromBody] Request_Department_DTO_Update dto)
        //{
        //    bool tmp = await Program.api_position.updateOne(dto.ID, dto.name, dto.code);
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
