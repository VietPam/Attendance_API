using Domain.Entities.User;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountInfo> AccountInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccountConfiguration());
        builder.ApplyConfiguration(new AccountInfoConfiguration());

        base.OnModelCreating(builder);

    }
}
