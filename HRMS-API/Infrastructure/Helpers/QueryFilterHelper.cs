using Domain.Entities.Common;
using Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Helpers;
public class QueryFilterHelper
{
    public static void AddQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).AddQueryFilter<Entity>(e => e.Deleted == null);
            }
        }
    }
}
