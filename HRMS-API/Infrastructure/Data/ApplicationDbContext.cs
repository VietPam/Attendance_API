using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Entities.Attendances;
using Domain.Entities.Departments;
using Infrastructure.Configuration;
using Infrastructure.Helpers;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<AttenState> AttenStates { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<CompanySetting> CompanySettings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccountConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        QueryFilterHelper.AddQueryFilters(builder);

        base.OnModelCreating(builder);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
    }
}
//add-migration addEntity -OutputDir Data\Migrations
