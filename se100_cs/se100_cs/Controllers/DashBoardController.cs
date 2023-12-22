using Microsoft.AspNetCore.Mvc;

namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    public class DashBoardController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getStats()
        {
            return Ok(await Program.api_dashboard.getStats());
        }
    }
}
