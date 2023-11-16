using System;
using testy.Application.Common.Interfaces;
using testy.Common.Constants;
using testy.Infrastructure.Implementations;

namespace testy.Infrastructure.Persistence.Factories
{
    public static class CryptoManagerFactory
    {
        public static ICryptoManager GetCryptoManager(this IServiceProvider provider)
            => new CryptoManager(AppSettingsConstants.CrytoSecretKey, AppSettingsConstants.CrytoInitializationVector);
    }
}
