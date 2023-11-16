using Microsoft.Extensions.Configuration;
using System;
using testy.Application.Common.Interfaces;

namespace testy.Infrastructure.Implementations
{
    public class JwtIssuerOptions : IJwtIssuerOptions
    {
        private readonly IConfiguration _configuration;

        public JwtIssuerOptions(IConfiguration configuration) => _configuration = configuration;

        public string JwtKey => _configuration["JwtIssuerOptions:JwtKey"];
        public string JwtIssuer => _configuration["JwtIssuerOptions:JwtIssuer"];
        public string JwtAudience => _configuration["JwtIssuerOptions:JwtAudience"];
        public double JwtExpireInMinutes => Convert.ToInt32(_configuration["JwtIssuerOptions:JwtExpireInMinutes"]);
    }
}
