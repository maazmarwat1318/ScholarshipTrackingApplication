using System.Text;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using InfrastructureLayer.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace MVCPresentationLayer.Configuration
{
    public static class Config
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

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration config)
        {
            JwtOptions jwtOptions = new();
            config.GetSection("JwtOptions").Bind(jwtOptions);
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwt"))
                        {
                            context.Token = context.Request.Cookies["jwt"];
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Account/Login");
                        return Task.CompletedTask;
                    },
                };
            });

            serviceCollection.AddAuthorization();

            return serviceCollection;
        }
    }
}
