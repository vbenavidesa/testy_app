using System.Collections.Generic;
using System.Security.Claims;

namespace testy.Application.Common.Interfaces
{
    public interface ITokenManager
    {
        string GenerateTokenFromClaims(ICollection<Claim> claims);
    }
}
