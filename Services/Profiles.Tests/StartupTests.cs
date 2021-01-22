using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Profiles.Tests.Fixtures;
using Sdk.Tests.Extensions;
using Xunit;

namespace Profiles.Tests
{
    public class ProfilesStartupTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ProfilesStartupTests(
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
    }
}