using Microsoft.EntityFrameworkCore;
using se100_cs.APIs;
using se100_cs.Model;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace se100_cs
{
    public class Program
    {
        public static MyDepartment api_department=new MyDepartment();
        public static MyPosition api_position=new MyPosition();
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File("mylog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        builder.WithOrigins("https://demo-signalr-reactjs.vercel.app")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
                });
                builder.Services.AddDbContext<DataContext>(options =>options.UseNpgsql(DataContext.configSql));
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();
                using (var scope = app.Services.CreateScope())
                {
                    IServiceProvider serviceProvider = scope.ServiceProvider;
                    DataContext dataContext = serviceProvider.GetRequiredService<DataContext>();
                    dataContext.Database.EnsureCreated();
                    await dataContext.Database.MigrateAsync();
                }
                app.UseCors();

                // Configure the HTTP request pipeline.
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    
                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseAuthorization(); 
                //app.MapGet("/", () => string.Format("Server E-Management of SE100- {0}", DateTime.Now));


                app.MapControllers();

                app.Run();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            
        }
    }
}