using Microsoft.Extensions.DependencyInjection;
using SESFIR.Mappers;

namespace SESFIR.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SQLDatabaseProfile));

            return services;
        }
    }
}
