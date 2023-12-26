using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_attendance")]
    public class SqlAttendance
    {
        [Key]
        public long ID { get; set; }
        public DateTime time { get; set; } = DateTime.UtcNow;
        public SqlEmployee employee { get; set; }
        public SqlState state { get; set; }
    }
}
