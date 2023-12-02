using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace se100_cs.Model
{
    [Table("tb_employee")]
    public class SqlEmployee
    {
        [Key]
        public long ID { get; set; }
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string token { get; set; } = "";
        public string fullName { get; set; } = "";
        public bool is_deleted { get; set; }=false;
        public string phoneNumber { get; set; } = "";
        public string avatar { get; set; } = "";
        public DateTime birth_day { get; set; }
        public bool gender { get; set; }=true;
        public string cmnd { get; set; } = "";
        public string address { get; set; } = "";
        public SqlRole? role { get; set; }
    }
}
