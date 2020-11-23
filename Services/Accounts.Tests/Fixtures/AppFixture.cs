using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Accounts.Tests.Fixtures
{
    public class AppFixture : IDisposable  
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public AppFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
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

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}