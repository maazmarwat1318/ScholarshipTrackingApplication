using InfrastructureLayer.Options;

namespace MVCPresentationLayer.Configuration
{
    internal static class InjectOptions
    {
        
        public static IServiceCollection AddOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddJwtOptions(config);
            return serviceCollection;
        }
        private static IServiceCollection AddJwtOptions(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection.Configure<JwtOptions>(config.GetSection("JwtOptions"));
        }
    }
}
