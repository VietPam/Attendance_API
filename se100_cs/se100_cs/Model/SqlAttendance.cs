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
        public List<SqlATDDetail> list_attendance { get; set; }
        //0 la dung gio
        //1 la tre gio
        //2 la khong di lam
        //public int status { get; set; } = 2;
        //public SqlEmployee? employee { get; set; }
    }
}
