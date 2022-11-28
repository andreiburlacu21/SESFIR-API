using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SESFIR.DTOs;
using SESFIR.Validators.Model.Validation;

namespace SESFIR.Configuration
{
    public static class ValidatorConfiguration
    {
        public static IServiceCollection AddValidatorConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AccountDTO>, AccountsValidation>();

            services.AddTransient<IValidator<BookingDTO>, BookingsValidation>();

            services.AddTransient<IValidator<LocationDTO>, LocationsValidation>();

            services.AddTransient<IValidator<ReviewDTO>, ReviewsValidation>();

            return services;
        }
    }
}
