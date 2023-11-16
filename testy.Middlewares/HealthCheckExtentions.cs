using HealthChecks.System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using testy.Application.Dtos.Response.HealthChecks;

namespace testy.Middlewares
{
    public static class HealthCheckExtentions
    {
        public static void RegisterHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration["ConnectionStrings:Default"])
                .AddDiskStorageHealthCheck(setup: delegate (DiskStorageOptions diskStorageOptions)
                {
                    diskStorageOptions.AddDrive(@"D:\", minimumFreeMegabytes: 20000);
                }, name: "Drive Health", HealthStatus.Unhealthy)
                .AddCheck<ResponseTimeHealthCheck>("Network speed test", null, new[] { "network" });
        }
    }
}
