using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_attendance")]
    public class SqlAttendance
    {
        [Key]
        public long ID { get; set; }
        public DateTime time { get; set; }= DateTime.Now;
        public attendance_status status { get; set; } = attendance_status.Absent;
        public SqlEmployee? employee { get; set; }
    }
    public enum attendance_status
    {
        OnTime,
        Late,
        Absent
    }
}
