using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        //phieu_luong
        // gui mail phieu luong
        
        [HttpGet]
        [Route("getPayrollDepartment")]
        public IActionResult get_payroll_department(string department_code, int start_month, int start_day, int end_month, int end_day, int year)
        {
            return Ok(Program.api_payroll.get_payroll_department(department_code,start_month,start_day,end_month,end_day,year));
        }
    }
}
