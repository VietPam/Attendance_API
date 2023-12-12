using Microsoft.AspNetCore.Mvc;
using se100_cs.APIs;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController:ControllerBase
    {
        [HttpGet]
        [Route("getListByDate")]
        public IActionResult getListByDate(int day, int month, int year=2023)
        {
            return Ok(Program.api_attendance.getListByDate(day, month, year));
        }
        [HttpGet]
        [Route("test")]
        public IActionResult test()
        {
            return Ok(Program.api_attendance.getListByDate(13, 12, 2023));
        }

        [HttpPost]
        [Route("createNew")]
        public async Task<IActionResult> markAttendance(string token)
        {
            long id = Program.api_employee.checkEmployee(token);
            return Ok(await Program.api_attendance.markAttendance(id));
        }
    }
}
