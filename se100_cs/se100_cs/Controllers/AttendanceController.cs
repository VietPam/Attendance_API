using Microsoft.AspNetCore.Mvc;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController:ControllerBase
    {
        [HttpGet]
        [Route("getListByDate")]
        public IActionResult getListByDate(int day=5 , int month=12, int year =2023)
        {

            return Ok(Program.api_attendance.getListByDate(day,month,year));
        }
    }
}
