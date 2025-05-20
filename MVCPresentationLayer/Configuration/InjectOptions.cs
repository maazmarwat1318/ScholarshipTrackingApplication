using InfrastructureLayer.Options;

namespace MVCPresentationLayer.Configuration
{
    internal static partial class Configuration
    {
        
        public static IServiceCollection AddOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddJwtOptions(config);
            serviceCollection.AddEmailServiceOptions(config);
            serviceCollection.AddCaptchaOptions(config);
            return serviceCollection;
        }
        private static IServiceCollection AddJwtOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.Configure<JwtOptions>(config.GetSection("JwtOptions"));
        }

        private static IServiceCollection AddEmailServiceOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.Configure<EmailServiceOptions>(config.GetSection("EmailServiceOptions"));
        }

        private static IServiceCollection AddCaptchaOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.Configure<CaptchaOptions>(config.GetSection("CaptchaOptions"));
        }
    }
}
