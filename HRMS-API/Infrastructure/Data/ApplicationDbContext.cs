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
    public DbSet<SqlAccount> Accounts { get; set; }
    public DbSet<SqlUser> User { get; set; }
    public DbSet<SqlDepartment> Departments { get; set; }
    public DbSet<SqlAttendance> Attendances { get; set; }
    public DbSet<SqlAttenState> AttenStates { get; set; }
    public DbSet<SqlPosition> Positions { get; set; }
    public DbSet<SqlRole> Roles { get; set; }
    public DbSet<SqlToken> Tokens { get; set; }
    public DbSet<SqlCompanySetting> CompanySettings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AccountConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
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
