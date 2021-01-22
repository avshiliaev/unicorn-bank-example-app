using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Approvals.Tests.Fixtures;
using Sdk.Tests.Extensions;
using Xunit;

namespace Approvals.Tests
{
    public class ApprovalsStartupTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApprovalsStartupTests(
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
            Assert.Equal(
                HttpStatusCode.ServiceUnavailable,
                response.StatusCode
            );
        }
    }
}