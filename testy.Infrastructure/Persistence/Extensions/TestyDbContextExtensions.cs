using Microsoft.Extensions.DependencyInjection;
using System;
using testy.Application.Common.Interfaces;

namespace testy.Infrastructure.Persistence.Extensions
{
    public static class TestyDbContextExtensions
    {
        public static ITestyDbContext GetTisurDbContext(this IServiceProvider provider)
        {
            var builder = provider.GetService<ITestyDbContextBuilder>();
            return builder.Build();
        }
    }
}
