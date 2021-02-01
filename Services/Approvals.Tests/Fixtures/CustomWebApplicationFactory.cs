using Approvals.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Sdk.Api.Interfaces;
using Sdk.Tests.Extensions;

namespace Approvals.Tests.Fixtures
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services
                    .AddTestMessageBus<ITransactionModel>()
                    .AddTestAuthentication()
                    .AddTestSqlDataBaseContext<ApprovalsContext>();
            });
        }
    }
}