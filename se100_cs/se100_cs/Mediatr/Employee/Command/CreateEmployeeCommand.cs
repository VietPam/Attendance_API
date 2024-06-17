using MediatR;
using static se100_cs.Controllers.EmployeeController;

namespace se100_cs.Mediatr.Employee.Command;

public class CreateEmployeeCommand : IRequest
{
    public CreateEmployeeCommand(Request_Employee_DTO employee, long position_id)
    {
        this.employee = employee;
        this.position_id = position_id;
    }
    public Request_Employee_DTO employee { get; set; }
    public long position_id { get; set; }
}
