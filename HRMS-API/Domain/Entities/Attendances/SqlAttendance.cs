using Domain.Entities.Common;
using Domain.Entities.Departments;

namespace Domain.Entities.Attendances;
public class SqlAttendance : Entity
{
    public DateTime Time { get; set; } = DateTime.UtcNow;
    public SqlUser User { get; set; } = null!;
    public SqlAttenState? State { get; set; }
}