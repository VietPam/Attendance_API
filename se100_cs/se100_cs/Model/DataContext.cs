using Microsoft.EntityFrameworkCore;

namespace se100_cs.Model
{
    public class DataContext: DbContext
    {

        public DbSet<SqlRole>? roles { get; set; }


        public static string configSql = "Host=ep-silent-hill-65750190.ap-southeast-1.postgres.vercel-storage.com:5432;Database=se100;Username=default;Password=8isowjIMlJG4";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(configSql);
        }
    }
}
