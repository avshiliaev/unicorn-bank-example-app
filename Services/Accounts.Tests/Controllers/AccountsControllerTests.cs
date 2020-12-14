using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Accounts.Tests.Fixtures;
using Xunit;

namespace Accounts.Tests.Controllers
{
    public class AccountsControllerTests : IClassFixture<AppFixture>
    {
        private readonly HttpClient _client;

        public AccountsControllerTests(AppFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task ShouldTurnDownIfNoProfileIdProvided()
        {
            // Arrange
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"/api/accounts/{Guid.Empty.ToString()}"
            );

            // Act
            var response = await _client.SendAsync(request);

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
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"/api/accounts/{Guid.NewGuid().ToString()}"
            );

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode
            );
        }
    }
}