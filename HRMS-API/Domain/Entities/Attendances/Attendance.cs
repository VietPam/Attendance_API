using Domain.Entities.Common;
using Domain.Entities.Departments;

namespace Domain.Entities.Attendances;
public class Attendance : Entity
{
    public DateTime Time { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = null!;
    public AttenState? State { get; set; }
}