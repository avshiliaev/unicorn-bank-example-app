using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Tests.Fixtures
{
    public static class ConfigureTestDataBase
    {
        public static IServiceCollection AddTestDataBaseContext<TContext>(this IServiceCollection services)
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

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TContext>();
                db.Database.EnsureCreated();
            }

            return services;
        }
    }
}