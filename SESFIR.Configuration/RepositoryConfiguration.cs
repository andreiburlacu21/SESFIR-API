using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SESFIR.DataAccess.ConnectionAccess;

namespace SESFIR.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ISQLDataAccess>(new SQLDataAccess(config.GetConnectionString("DataBase")));
            return services;
        }
    }
}
