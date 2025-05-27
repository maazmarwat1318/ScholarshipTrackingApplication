using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebAPI.Swagger;

namespace WebAPI.Configuration
{
    internal static partial class Configuration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Scholarship Application Tracking"
                });
                options.UseInlineDefinitionsForEnums();
                options.EnableAnnotations();
                options.AddSecurityDefinition(
                    "Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header - Bearer Scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            }

            );


        }
    }
}
