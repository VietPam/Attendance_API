using MediatR;
using se100_cs.Mediatr.Employee.Command;

namespace se100_cs.Mediatr.Employee.Handler;

public sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
{
    public Task Handle(CreateEmployeeCommand _DTO, CancellationToken cancellationToken)
    {
        return Program.api_employee.createNew(_DTO.employee.email, _DTO.employee.fullName, _DTO.employee.phoneNumber, _DTO.employee.birth_day, _DTO.employee.gender, _DTO.employee.cmnd, _DTO.employee.address, _DTO.employee.avatar, _DTO.position_id);
    }
}