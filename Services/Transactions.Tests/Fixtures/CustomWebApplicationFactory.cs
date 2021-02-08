using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Sdk.Interfaces;
using Sdk.Tests.Extensions;
using Transactions.Persistence;

namespace Transactions.Tests.Fixtures
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
                    .AddTestSqlDataBaseContext<TransactionsContext>();
            });
        }
    }
}