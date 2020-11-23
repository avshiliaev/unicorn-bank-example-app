using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Accounts.Dto;
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
        public async Task CreateNewAccountWrongData()
        {
            // Arrange
            var newAccountRequest = new AccountDto
            {
                Balance = 1.0f
            };
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "/api/accounts/"
            )
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(newAccountRequest),
                    Encoding.UTF8,
                    "application/json"
                )
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode
            );
        }

        [Fact]
        public async Task CreateNewAccountRightData()
        {
            // Arrange
            var newAccountRequest = new AccountDto
            {
                ProfileId = Guid.NewGuid().ToString()
            };
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "/api/accounts/"
            )
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(newAccountRequest),
                    Encoding.UTF8,
                    "application/json"
                )
            };

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