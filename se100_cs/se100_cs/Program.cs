using Serilog;

namespace se100_cs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            try
            {

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
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();
                app.UseCors();

                // Configure the HTTP request pipeline.
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    
                app.UseHttpsRedirection();

                app.UseAuthorization();
                app.MapGet("/", () => string.Format("Server E-Management of SE100- {0}", DateTime.Now));


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