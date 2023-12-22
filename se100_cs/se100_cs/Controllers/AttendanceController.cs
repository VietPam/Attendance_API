using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using se100_cs.APIs;
using static se100_cs.APIs.MyEmployee;

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

        [HttpPost]
        [Route("checkin")]
        public async Task<IActionResult> markAttendance([FromBody] _Token token)
        {
            long id = Program.api_employee.checkEmployee(token.token);
            return Ok(await Program.api_attendance.markAttendance(id));
        }
        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> check([FromBody] _Token token)
        {
            return Ok(await Program.api_attendance.check(token.token));
        }

        [HttpPut]
        [Route("test/update_attendance_admin")]
        public async Task<IActionResult> update_attendance_admin(int status)
        {
            return Ok(await Program.api_attendance.update_attendance_admin(status));
        }


        [HttpGet]
        [Route("getAll")]
        public IActionResult getAll(int limit_day)
        {
            string data = JsonConvert.SerializeObject(Program.api_attendance.getAll(limit_day));
            return Ok(data);
        }
    }
}
