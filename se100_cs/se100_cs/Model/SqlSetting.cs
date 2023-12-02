using System.ComponentModel.DataAnnotations.Schema;

namespace se100_cs.Model
{
    [Table("tb_setting")]
    public class SqlSetting
    {
        public int start_time_hour { get; set; } = 8;
        public int start_time_minute { get; set; } = 15;
        public int salary_per_coef { get; set; } = 200000;
        public int payment_date { get; set; } = 15;
    }
}
