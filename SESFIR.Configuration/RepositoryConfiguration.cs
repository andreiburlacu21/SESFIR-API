using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SESFIR.DataAccess.ConnectionAccess;
using SESFIR.DataAccess.Factory;

namespace SESFIR.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services, IConfiguration config)
        {

            services.AddSingleton<ISQLDataAccess>(new SQLDataAccess(config.GetConnectionString("DataBase") ?? throw new ArgumentNullException("config")));

            services.AddSingleton<ISQLDataFactory, SQLDataFactory>();

            return services;
        }
    }
}
