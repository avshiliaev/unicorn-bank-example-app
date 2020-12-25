using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Sdk.Tests.Extensions;
using Transactions.Tests.Fixtures;
using Xunit;

namespace Transactions.Tests.Controllers
{
    public class TransactionsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TransactionsControllerTests(
            CustomWebApplicationFactory<Startup> factory
        )
        {
            _client = factory.GetTestHttpClientClient();
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