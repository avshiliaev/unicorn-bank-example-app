using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Accounts.Tests.Fixtures;
using Sdk.Tests.Extensions;
using Xunit;

namespace Accounts.Tests.Controllers
{
    public class AccountsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountsControllerTests(
            CustomWebApplicationFactory<Startup> factory
        )
        {
            _client = factory.GetTestHttpClientClient();
        }

        [Fact]
        public async Task ShouldNot_GetAsync_Invalid()
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
        public async Task Should_GetAsync_Valid()
        {
            // Arrange
            var requestUrl = "/api/accounts";

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