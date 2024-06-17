using MediatR;
using Microsoft.AspNetCore.Mvc;
using se100_cs.Mediatr.Employee.Command;
using static se100_cs.Controllers.EmployeeController;

namespace se100_cs.Mediatr.Employee.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeV2Controller : ControllerBase
{
    public ISender sender { get; }
    public EmployeeV2Controller(ISender sender)
    {
        this.sender = sender;
    }

    [HttpPost]
    [Route("createNew")]
    public async Task<ActionResult> createNew(Request_Employee_DTO _DTO, long position_id)
    {
        await sender.Send(new CreateEmployeeCommand(_DTO, position_id));
        return Ok();
    }
}
