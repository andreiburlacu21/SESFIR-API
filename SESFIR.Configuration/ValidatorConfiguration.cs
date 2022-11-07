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
            services.AddTransient<IValidator<AccountsDTO>, AccountsValidation>();

            services.AddTransient<IValidator<BookingsDTO>, BookingsValidation>();

            services.AddTransient<IValidator<LocationsDTO>, LocationsValidation>();

            services.AddTransient<IValidator<ReviewsDTO>, ReviewsValidation>();

            return services;
        }
    }
}
