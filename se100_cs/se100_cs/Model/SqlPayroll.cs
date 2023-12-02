using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_payroll")]
    public class SqlPayroll
    {
        [Key]
        public long ID { get; set; }
        public long salary { get; set; } = 0;
        public DateTime receive_date { get; set; }
        //public bool is_received { get; set; }
        public SqlEmployee? employee { get; set; }
    }
}
