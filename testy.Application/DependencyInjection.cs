using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using testy.Common.Constants;

namespace testy.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettingsConstants.SetConfiguration(configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
