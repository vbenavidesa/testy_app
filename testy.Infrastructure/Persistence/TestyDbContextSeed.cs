using System.Threading.Tasks;
using testy.Application.Common.Interfaces;

namespace testy.Infrastructure.Persistence
{
  public static class TestyDbContextSeed
    {
        public static async Task SeedBaseData(ITestyDbContext context, ICryptoManager cryptoManager)
        {
            await SeedDataAsync(context, cryptoManager);
        }

        private static async Task SeedDataAsync(ITestyDbContext context, ICryptoManager cryptoManager)
        {
            // Here we can create any kind of seeds for the database that we need
        }
    }
}
