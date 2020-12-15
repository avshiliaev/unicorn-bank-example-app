using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Accounts.Handlers;
using Accounts.Tests.Fixtures;
using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;
using Sdk.Api.Events;
using Xunit;

namespace Accounts.Tests.Controllers
{
    public class AccountsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup>
            _factory;

        public AccountsControllerTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task ShouldTurnDownIfNoProfileIdProvided()
        {
            // Arrange
            var requestUrl = $"/api/accounts/{Guid.Empty.ToString()}";

            // Act
            var response = await _client.GetAsync(requestUrl);

            // Assert
            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode
            );

        }

        [Fact]
        public async Task CreateNewAccountRightData()
        {
            // Arrange
            var requestUrl = $"/api/accounts/{Guid.NewGuid().ToString()}";

            // Act
            var response = await _client.GetAsync(requestUrl);

            // Assert
            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode
            );
        }
    }
}