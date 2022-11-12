using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SESFIR.Services.Model.Service;
using SESFIR.Services.Model.Service.Contracts;

namespace SESFIR.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IServiceAccounts, AccountsService>();

            services.AddTransient<IServiceBookings, BookingsService>();

            services.AddTransient<IServiceLocations, LocationsService>();

            services.AddTransient<IServiceReviews, ReviewsService>();

            return services;
        }
    }
}
