using Microsoft.Extensions.Configuration;
using System;

namespace testy.Common.Constants
{
    public static class AppSettingsConstants
    {
        private static IConfiguration _configuration = null;
        static AppSettingsConstants() { }
        public static void SetConfiguration(IConfiguration configuration) => _configuration = configuration;
        public static bool UseInMemoryDatabase => Convert.ToBoolean(_configuration["UseInMemoryDatabase"]);
        public static string DefaultConnection => _configuration["ConnectionStrings:Default"];
        public static string CrytoSecretKey => _configuration["CrytoSecretKey"];
        public static string CrytoInitializationVector => _configuration["CrytoInitializationVector"];
    }
}
