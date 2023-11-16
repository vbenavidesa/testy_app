using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using testy.Application.Common.Interfaces;

namespace testy.Infrastructure.Implementations
{
    public class TokenManager : ITokenManager
    {
        private readonly IJwtIssuerOptions _options;

        public TokenManager(IJwtIssuerOptions options)
        {
            _options = options;
        }

        public string GenerateTokenFromClaims(ICollection<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_options.JwtExpireInMinutes);

            IdentityModelEventSource.ShowPII = true;

            var token = new JwtSecurityToken(
                _options.JwtIssuer,
                _options.JwtAudience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
