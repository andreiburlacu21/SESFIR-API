using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SESFIR.DataAccess.Data.Interfaces;
using SESFIR.Mappers;

namespace SESFIR.Configuration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SQLDatabaseProfile));
            services.AddSingleton(provider => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CustomProfileWithEntities(provider.GetService<IBookingsRepository>(), provider.GetService<ILocationsRepository>()));

                cfg.AddProfile(typeof(SQLDatabaseProfile));

            }).CreateMapper());

            return services;
        }
    }
}
