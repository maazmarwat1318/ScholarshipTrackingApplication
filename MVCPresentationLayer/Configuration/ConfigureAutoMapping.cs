using System.Text;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using InfrastructureLayer.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using MVCPresentationLayer.MappingProfiles;
namespace MVCPresentationLayer.Configuration
{
    internal static partial class Configuration
    {

        public static IServiceCollection ConfigureAutoMapping(this IServiceCollection serviceCollection)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentMappingProfile>();
                cfg.AddProfile<DegreeMappingProfile>();
            });

            #if DEBUG
            try
            {
                configuration.AssertConfigurationIsValid();
            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            #endif
            var mapper = configuration.CreateMapper();
            serviceCollection.AddSingleton(mapper);
            return serviceCollection;
        }
    }
}
