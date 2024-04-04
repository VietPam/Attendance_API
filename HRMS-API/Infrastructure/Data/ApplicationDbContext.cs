using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountInfo> AccountInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        SetTableNamesAsSingle(builder);

        base.OnModelCreating(builder);
        //builder.Entity<Order>(ConfigureOrder);
        //builder.Entity<Product>(ConfigureProduct);

        //builder.Entity<ProductWishlist>(ConfigureProductWishlist);
    }
    private static void SetTableNamesAsSingle(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
        }
    }
}
