using System.Collections.Generic;
using Accounts.Persistence;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sdk.Persistence.Extensions;
using Sdk.Tests.Extensions;

namespace Accounts.Tests.Fixtures
{
    public class TestStartup : Startup
    {
        public TestStartup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
            : base(configuration, webHostEnvironment)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services
                .AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly);
            services.RemoveAll(typeof(AccountsContext));
            services.AddDbContext<AccountsContext>(
                options =>
                    options.UseInMemoryDatabase("test")
            );
        }
    }
}