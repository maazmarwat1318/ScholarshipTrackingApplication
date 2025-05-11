using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using InfrastructureLayer.Options;
namespace MVCPresentationLayer.Configuration
{
    public static class Config
    {
        public static IServiceCollection ConfigureDatabaseLayer(this IServiceCollection serviceCollection, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DatabaseConnection");
            return serviceCollection.AddDbContext<StaDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration config)
        {
            JwtOptions jwtOptions = new();
            config.GetSection("JwtOptions").Bind(jwtOptions);
            return serviceCollection;
        }
    }
}
