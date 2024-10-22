using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Sdk.Api.Interfaces;
using Sdk.Tests.Extensions;

namespace Notifications.Tests.Fixtures
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services
                    .AddTestMessageBus<INotificationModel>()
                    .AddTestAuthentication()
                    .AddTestMongoDataBaseContext();
            });
        }
    }
}