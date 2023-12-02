using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_role")]

    public class SqlRole
    {
        [Key]
        public long ID { get; set; }
        public string code { get; set; } = "";
        public string name { get; set; } = "";
        //public string des { get; set; } = "";
        //public string note { get; set; } = "";
        public bool isDeleted { get; set; } = false;
    }
}
