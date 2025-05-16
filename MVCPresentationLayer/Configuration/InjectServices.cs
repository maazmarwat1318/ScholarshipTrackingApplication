using ApplicationLayer.Interface;
using ApplicationLayer.Service;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DataLayer.Repository;
using InfrastructureLayer.Service;

namespace MVCPresentationLayer.Configuration
{
    internal static class InjectServices
    {
        
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddDataLayerRepositories(config);
            serviceCollection.AddInfrastructureLayerServices(config);
            serviceCollection.AddApplicationLayerServices(config);
            return serviceCollection;
        }
        private static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddSingleton<ICaptchaVerificationService, CaptchaVerificationService>();
            serviceCollection.AddSingleton<IJwtService, JwtService>();
            serviceCollection.AddSingleton<ICrypterService, CrypterService>();
            serviceCollection.AddSingleton<IEmailService, EmailService>();
            return serviceCollection;
        }

        private static IServiceCollection AddApplicationLayerServices(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddScoped<IAccountService, AccountService>();
            return serviceCollection;
        }

        private static IServiceCollection AddDataLayerRepositories(this IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            return serviceCollection;
        }
    }
}
