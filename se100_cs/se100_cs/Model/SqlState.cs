using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_atd_status")]
    public class SqlState
    {
        [Key]
        public long ID { get; set; }
        public string code { get; set; } = "Absent";
        //Absent, Late, OnTime
    }
}
