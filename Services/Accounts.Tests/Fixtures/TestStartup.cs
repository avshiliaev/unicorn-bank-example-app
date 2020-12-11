using Accounts.Tests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Tests.Fixtures
{
    public class TestStartup : Startup
    {
        public TestStartup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services
                .AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly);
            var accountsRepositoryMock = new AccountsRepositoryMockFactory().GetInstance();
            services.AddTransient(provider => accountsRepositoryMock.Object);
        }
    }
}