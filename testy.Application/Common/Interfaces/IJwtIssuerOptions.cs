namespace testy.Application.Common.Interfaces
{
    public interface IJwtIssuerOptions
    {
        string JwtKey { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        double JwtExpireInMinutes { get; }
    }
}
