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
        public IActionResult get_payroll_department(string department_code="all",string start ="01-01-2024",string end = "08-01-2024")
        {
            return Ok(Program.api_payroll.get_payroll_department(department_code,start,end));
        }
    }
}
