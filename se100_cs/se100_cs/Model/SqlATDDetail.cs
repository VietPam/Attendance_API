using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_attendance_detail")]
    public class SqlATDDetail
    {
        [Key]
        public long ID { get; set; }
        public SqlAttendance attendance { get; set; }
        public int status { get; set; } = 2;
        public long employeeId { get; set; }
    }
}
