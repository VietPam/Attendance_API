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
                if(tmp )
                {
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
