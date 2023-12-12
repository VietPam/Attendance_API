using Microsoft.AspNetCore.Mvc;
using se100_cs.APIs;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController:ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public IActionResult get()
        {
            return Ok(Program.api_setting.get());
        }
        [HttpPut]
        [Route("updateOne")]
        public async Task<IActionResult> updateOne([FromBody] MySetting.Setting_DTO dto)
        {
            bool tmp = await Program.api_setting.updateOne(dto.company_code, dto.company_name, dto.start_time_hour, dto.start_time_minute, dto.salary_per_coef, dto.payment_date);
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
