using System.Linq;
using Accounts.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sdk.Api.Interfaces;
using Sdk.Tests.Mocks;

namespace Accounts.Tests.Fixtures
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AccountsContext>)
                    )
                );
                services.Remove(services.SingleOrDefault(
                        d => d.ServiceType == typeof(IPublishEndpoint)
                    )
                );

                services
                    .AddDbContext<AccountsContext>(
                        options => options.UseInMemoryDatabase("InMemoryDbForTesting")
                    )
                    .AddTransient(
                        provider => new PublishEndpointMockFactory<IAccountModel>().GetInstance().Object
                    );

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AccountsContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();
                }
            });
        }
    }
}