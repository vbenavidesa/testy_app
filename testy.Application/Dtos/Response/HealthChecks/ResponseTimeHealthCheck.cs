using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace testy.Application.Dtos.Response.HealthChecks
{
    public class ResponseTimeHealthCheck : IHealthCheck
    {
        #region constructor
        private Ping ping = new Ping();
        private readonly IConfiguration config;
        public ResponseTimeHealthCheck(IConfiguration config)
        {
            this.config = config;
        }
        #endregion

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var url = config.GetSection("Urls").GetSection("Base").Value;
            
            var response = ping.Send(url, 15000);
            if (response.Status != IPStatus.Success)
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy($"The response time is unhleathy. ({ response.Status })")
                    );
            }
            else if (response.RoundtripTime > 300)
            {
                return Task.FromResult(
                    HealthCheckResult.Degraded($"The response time is degraded at { response.RoundtripTime }ms for the application.")
                    );
            }
            else
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy($"The response time is healthy at { response.RoundtripTime }ms for the application.")
                    );
            }
        }
    }
}
