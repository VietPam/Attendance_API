using Microsoft.EntityFrameworkCore;
using se100_cs.Model;

namespace se100_cs.APIs
{
    public class MyState
    {
        public MyState() { }
        public async Task initAsync()
        {
            using (DataContext context = new DataContext())
            {
                bool tmp = false;
                List<SqlState> state = context.ATD_state.Where(s => s.code == "Absent" || s.code == "Late" || s.code == "OnTime").ToList();
                if (!state.Where(s => s.code == "Absent").Any())
                {
                    SqlState absent = new SqlState();
                    absent.code = "Absent";
                    context.ATD_state.Add(absent);
                    tmp = true;
                }
                if (!state.Where(s => s.code == "Late").Any())
                {
                    SqlState Late = new SqlState();
                    Late.code = "Late";
                    context.ATD_state.Add(Late);
                    tmp = true;
                }
                if (!state.Where(s => s.code == "OnTime").Any())
                {
                    SqlState OnTime = new SqlState();
                    OnTime.code = "OnTime";
                    context.ATD_state.Add(OnTime);
                    tmp = true;
                }
                if (tmp)
                {
                    await context.SaveChangesAsync();
                }
            }
        }

        public string getState(DateTime time)
        {
            string code = "Internal Error";
            int hour = time.Hour;
            int minute = time.Minute;
            int setting_hour, setting_minute;
            using (DataContext context = new DataContext())
            {
                SqlSetting? setting = context.settings.AsNoTracking().FirstOrDefault();
                if (setting == null) {
                    return code;
                }
                setting_hour = setting!.start_time_hour;
                setting_minute = setting!.start_time_minute;
                if (hour == 23 && minute == 59)
                {
                    return "Absent";
                }
                else if (hour * 60 + minute > setting_hour * 60 + setting_minute)// trễ
                {
                    return "Late";
                }
                else 
                {
                    return "OnTime";
                }
            }
            
        }     
    }
}
