using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace SESFIR.Configuration;

public static class JWTConfiguration
{
    public static IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration config)
    {
         services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
           {
               jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetConnectionString("MySecretKey"))),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.FromMinutes(5)
               };
           });

        return services;
    }
}
