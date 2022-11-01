using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Configuration
{
    public static class ValidatorConfiguration
    {
        public static IServiceCollection AddValidatorConfiguration(this IServiceCollection services)
        {
            return services;
        }
    }
}
