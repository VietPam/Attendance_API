using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_attendance")]
    public class SqlAttendance
    {
        [Key]
        public long ID { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public List<SqlATDDetail>? list_attendance { get; set; } = new List<SqlATDDetail>();
    }
}
