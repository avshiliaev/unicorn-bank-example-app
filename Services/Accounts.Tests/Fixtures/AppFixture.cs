using System;
using System.IO;
using System.Net.Http;
using Accounts.Persistence;
using Accounts.Persistence.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Tests.Fixtures
{
    public class AppFixture : IDisposable
    {
        private readonly TestServer _server;

        public AppFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<TestStartup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\Accounts")
                    );

                    config.AddJsonFile("appsettings.json");
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }

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
            services.AddTransient<DbContext, MemorySqlDbContext>();
        }
    }

    public class MemorySqlDbContext : AccountsContext
    {
        public MemorySqlDbContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {
        }

        public string Schema => "TEST";
        public DbSet<AccountEntity> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountEntity>().ToTable("Accounts");
            base.OnModelCreating(modelBuilder);
        }
    }
}