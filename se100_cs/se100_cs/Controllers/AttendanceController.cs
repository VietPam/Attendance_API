using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using se100_cs.APIs;
using static se100_cs.APIs.MyEmployee;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        //[HttpGet]
        //[Route("getListByDate")]
        //public IActionResult getListByDate(int day1, int month, int year=2023)
        //{
        //    //return Ok(Program.api_attendance.getListByDate(day, month, year));
        //    return Ok();
        //}
        DateTime today = DateTime.Today;
         

        [HttpPost]
        [Route("checkin")]
        public async Task<IActionResult> markAttendance([FromBody] _Token token)
        {
            long id = Program.api_employee.checkEmployee(token.token);
            return Ok(await Program.api_attendance.markAttendance(id));
        }
        [HttpPost]
        [Route("check")]
        public IActionResult check([FromBody] _Token token)
        {
            long id = Program.api_employee.checkEmployee(token.token);

            return Ok(Program.api_attendance.check(id));

        }

       

        [HttpGet]
        [Route("getList")]
        public IActionResult getList(string date = "2024-01-04", string department_code = "all")
        {
            return Ok(Program.api_attendance.getList(date,department_code));
        }


    }
}
