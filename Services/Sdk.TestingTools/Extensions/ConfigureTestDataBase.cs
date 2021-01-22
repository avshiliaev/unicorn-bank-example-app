using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sdk.Tests.Extensions
{
    public static class ConfigureTestDataBase
    {
        public static IServiceCollection AddTestSqlDataBaseContext<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.Remove(services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<TContext>)
                )
            );
            services
                .AddDbContext<TContext>(
                    options => options.UseInMemoryDatabase("InMemoryDbForTesting")
                );

            var sp = services.BuildServiceProvider();

            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Trace)
            );

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TContext>();
                db.Database.EnsureCreated();
            }

            return services;
        }

        public static IServiceCollection AddTestMongoDataBaseContext(this IServiceCollection services)
        {
            return services;
        }
    }
}