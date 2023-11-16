using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using testy.Application.Common.Interfaces;
using testy.Common.Constants;

namespace testy.Infrastructure.Persistence.Factories
{
    public class TestyDbContextBuilder : ITestyDbContextBuilder
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;
        public IConfiguration Configuration { get; }
        public TestyDbContextBuilder(ICurrentUserService currentUserService, IConfiguration configuration, IDomainEventService domainEventService)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            Configuration = configuration;
        }

        public ITestyDbContext Build()
        {
            var options = GetContextOptions<TestyDbContext>(AppSettingsConstants.DefaultConnection, AppSettingsConstants.UseInMemoryDatabase);
            var context = new TestyDbContext(options, Configuration, _currentUserService, _domainEventService);
            return context;
        }

        private static DbContextOptions<TContext> GetContextOptions<TContext>(string connectionString, bool isUseInMemoryDatabase) where TContext : DbContext
        {
            if (isUseInMemoryDatabase)
                return new DbContextOptionsBuilder<TContext>()
                    .UseInMemoryDatabase("TestyDb")
                    .Options;

            return new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;
        }
    }
}
