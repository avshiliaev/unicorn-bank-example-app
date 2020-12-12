using System.Collections.Generic;
using Accounts.Persistence.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;

namespace Accounts.Tests.Fixtures
{
    public class TestStartup : Startup
    {
        private readonly List<AccountEntity> _accountEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 3.ToGuid(),
                Balance = 1,
                ProfileId = 2.ToGuid(),
                Version = 0
            }
        };

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
            var accountsRepositoryMock = new RepositoryMockFactory<AccountEntity>(_accountEntities).GetInstance();
            services.AddTransient(provider => accountsRepositoryMock.Object);
        }
    }
}