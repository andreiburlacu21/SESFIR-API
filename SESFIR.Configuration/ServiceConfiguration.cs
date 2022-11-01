using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SESFIR.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
    }
}
