using System.Text;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using InfrastructureLayer.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Configuration
{
    internal static partial class Configuration
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DatabaseConnection");
            return serviceCollection.AddDbContext<StaDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        public static IServiceCollection ConfigureHttpClient(this IServiceCollection serviceCollection)
        {

            return serviceCollection.AddHttpClient();
        }

        public static IServiceCollection ConfigureCORS(this IServiceCollection serviceCollection)
        {
            
            return serviceCollection.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        public static IServiceCollection SupressAutoValidationResponse(this IServiceCollection serviceCollection)
        {

            return serviceCollection.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

    }
}
