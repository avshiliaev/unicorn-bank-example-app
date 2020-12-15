using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sdk.Persistence.Extensions
{
    public static class MigrationManager
    {
        public static IApplicationBuilder UpdateDatabase<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TContext>())
                {
                    if (context?.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                        context?.Database.Migrate();
                }
            }

            return app;
        }
    }
}