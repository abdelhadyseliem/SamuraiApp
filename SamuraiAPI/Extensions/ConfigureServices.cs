using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;

namespace SamuraiAPI.Extensions
{
    public static class ConfigureServices
    {
        public static IConfiguration? Configuration { get; private set; }

        public static void SetupServices(this WebApplicationBuilder builder)
        {
            Configuration = builder.Configuration;

            UseControllers(builder);
            UseOpenApi(builder);
            UseDbContext(builder);
        }

        public static void UseControllers(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
        }

        public static void UseOpenApi(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void UseDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SamuraiContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SamuraiDb"));
                options.EnableSensitiveDataLogging();
            });
        }
    }
}
