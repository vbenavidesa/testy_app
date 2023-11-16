using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using testy.Application.Common.Interfaces;
using testy.Infrastructure.Extensions;
using testy.Infrastructure.Implementations;
using testy.Infrastructure.Persistence.Extensions;
using testy.Infrastructure.Persistence.Factories;
using testy.Middlewares;

namespace testy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtIssuerOptions = new JwtIssuerOptions(configuration);

            services.AddSingleton<IJwtIssuerOptions>(jwtIssuerOptions);
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton(provider => provider.GetCryptoManager());

            services.AddScoped<ITestyDbContextBuilder, TestyDbContextBuilder>();
            services.AddScoped(provider => provider.GetTisurDbContext());
            
            services.RegisterApplicationServices(Assembly.GetExecutingAssembly());

            services.AddJWTAuthentication(jwtIssuerOptions);

            return services;
        }
    }
}
