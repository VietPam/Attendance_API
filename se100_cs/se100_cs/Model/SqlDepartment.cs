using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace se100_cs.Model
{
    [Table("tb_department")]
    public class SqlDepartment
    {
        [Key]
        public long ID { get; set; }
        public string name { get; set; } = "";
        public string code { get; set; } = "";
        public bool isDeleted { get; set; }=false;
    }
}
