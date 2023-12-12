using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using static se100_cs.APIs.MyPosition;

namespace se100_cs.APIs
{
    public class MySetting
    {
        public MySetting() { }
        public class Setting_DTO
        {
            public string company_name { get; set; } = "";
            public string company_code { get; set; } = "";
            public int start_time_hour { get; set; } = 0;
            public int start_time_minute { get; set; } = 0;
            public int salary_per_coef { get; set; } = 0;
            public int payment_date { get; set; } = 0;
        }
        public Setting_DTO get()
        {
            using (DataContext context = new DataContext())
            {
                Setting_DTO item = new Setting_DTO();
                SqlSetting? setting = new SqlSetting();
                try
                {
                    setting = context.settings!.FirstOrDefault();

                    if (setting == null)
                    {
                        return new Setting_DTO();
                    }
                }
                catch (Exception e)
                {
                    return new Setting_DTO();
                };

                item.company_code = setting.company_code;
                item.company_name = setting.company_name;
                item.start_time_hour = setting.start_time_hour;
                item.start_time_minute = setting.start_time_minute;
                item.salary_per_coef = setting.salary_per_coef;
                item.start_time_hour = setting.start_time_hour;
                return item;
            }
        }
        public async Task<bool> updateOne(string company_code, string company_name, int start_time_hour, int start_time_minute, int salary_per_coef, int payment_date)
        {
            if (string.IsNullOrEmpty(company_code))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlSetting? company = context.settings!.FirstOrDefault();
                if (company == null)
                {
                    return false;
                }
                else
                {
                    company.company_code = company_code;
                    company.company_name = company_name;
                    company.start_time_hour = start_time_hour;
                    company.start_time_minute = start_time_minute;
                    company.salary_per_coef = salary_per_coef;
                    company.payment_date = payment_date;
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
    }
}
