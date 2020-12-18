using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Sdk.Tests.Extensions
{
    public static class ConfigureTestHttpClient
    {
        public static HttpClient GetTestHttpClientClient<TStartup>(
            this WebApplicationFactory<TStartup> factory
        )
            where TStartup : class
        {
            var client = factory
                .WithWebHostBuilder(b =>
                {
                    b.ConfigureAppConfiguration((context, conf) =>
                    {
                        conf.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                            "appsettings.Test.json"));
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Test");

            return client;
        }
    }
}