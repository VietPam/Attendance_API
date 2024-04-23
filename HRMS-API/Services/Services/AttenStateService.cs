using Domain.Entities.Attendances;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Services;

public class AttenStateService(ApplicationDbContext context)
{
    public async Task InitAsync()
    {
        bool NeedToSave = false;
        List<string> stateCodes = ["Absent", "Late", "OnTime"];

        List<AttenState> state = await context.AttenStates.Where(s => stateCodes.Contains(s.Code)).ToListAsync();

        foreach (string stateCode in stateCodes)
        {
            if (!state.Any(s => s.Code == stateCode))
            {
                context.AttenStates.Add(new AttenState { Code = stateCode, Name = stateCode });
                NeedToSave = true;
            }
        }

        if (NeedToSave)
        {
            await context.SaveChangesAsync();
        }
    }
}
