using Microsoft.EntityFrameworkCore;

namespace se100_cs.Model
{
    public class DataContext: DbContext
    {
        public static Random random= new Random();
        public DbSet<SqlEmployee>? employees { get; set; }
        public DbSet<SqlDepartment> departments { get; set; }
        public DbSet<SqlPayroll> payrolls { get; set; }
        public DbSet<SqlAttendance>? attendances { get; set; }
        public DbSet<SqlPosition>? positions { get; set; }
        public DbSet<SqlRole>? roles { get; set; }
        public DbSet<SqlSetting> settings { get; set; }

        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(chars.Length)]).ToArray());
        }
        public static string configSql = "Host=ep-silent-hill-65750190.ap-southeast-1.postgres.vercel-storage.com:5432;Database=se100;Username=default;Password=8isowjIMlJG4";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(configSql);
        }
    }
}
