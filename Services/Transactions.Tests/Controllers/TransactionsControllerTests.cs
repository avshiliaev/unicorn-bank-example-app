using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Sdk.Tests.Extensions;
using Transactions.Tests.Fixtures;
using Transactions.ViewModels;
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
        public async Task Should_ResponseToHealthCheck()
        {
            // Arrange
            var requestUrl = "/health";

            // Act
            var response = await _client.GetAsync(requestUrl);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.OK || 
                response.StatusCode == HttpStatusCode.ServiceUnavailable
            );
        }

        [Fact]
        public async Task ShouldNot_GetAsync_NoParameters()
        {
            // Arrange
            var requestUrl = $"/api/transactions";

            // Act
            var response = await _client.GetAsync(requestUrl);

            // Assert
            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode
            );
        }

        [Fact]
        public async Task ShouldNot_GetAsync_NoAccountToLinkTo()
        {
            // Arrange
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["accountId"] = Guid.NewGuid().ToString();
            query["info"] = "info";
            query["amount"] = 1f.ToString(CultureInfo.InvariantCulture);
            var queryString = query.ToString();
            var url = $"/api/transactions?{queryString}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode
            );
        }
    }
}