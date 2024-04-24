using HRMS_API.Middlewares;
using Infrastructure;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                 .WriteTo.File("logs/mylog.txt", rollingInterval: RollingInterval.Day)
                 .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddServices();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("HTTPSystem", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("Grpc-Status", "Grpc-Encoding", "Grpc-Accept-Encoding");
            });
        });
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();
        await SeedDatabase.SeedAsync(app.Services);
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();

        app.UseCors("HTTPSystem");

        app.UseTokenMiddleware();
        app.UseExceptionHandlerMiddleware();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
// global filter soft delete
// sửa lại primitives theo trần đồng, chia nhiều interfaces
// ở api register cho thêm role, nếu người truyền vào có role trong token là admin thì lấy role theo admin,
// không phải admin thì truyền vào role là user