using AutoMapper;
using WebAPI.MappingProfiles;
namespace WebAPI.Configuration
{
    internal static partial class Configuration
    {

        public static IServiceCollection ConfigureAutoMapping(this IServiceCollection serviceCollection)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentMappingProfile>();
                cfg.AddProfile<DegreeMappingProfile>();
                cfg.AddProfile<ScholarshipModeratorMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
            });

            #if DEBUG
            try
            {
                configuration.AssertConfigurationIsValid();
            } catch (Exception)
            {
                throw;
            }
            #endif
            var mapper = configuration.CreateMapper();
            serviceCollection.AddSingleton(mapper);
            return serviceCollection;
        }
    }
}
