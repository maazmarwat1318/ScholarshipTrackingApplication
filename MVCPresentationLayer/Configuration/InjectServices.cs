using InfrastructureLayer.Interface;
using InfrastructureLayer.Options;
using InfrastructureLayer.Service;

namespace MVCPresentationLayer.Configuration
{
    internal static class InjectServices
    {
        
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddInfrastructureLayerServices(config);
            serviceCollection.AddApplicationLayerServices(config);
            return serviceCollection;
        }
        private static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddSingleton<IJwtService, JwtService>();
            serviceCollection.AddSingleton<ICrypterService, CrypterService>();
            return serviceCollection;
        }

        private static IServiceCollection AddApplicationLayerServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            return serviceCollection;
        }
    }
}
