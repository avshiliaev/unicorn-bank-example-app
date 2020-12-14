using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Accounts.Tests.Fixtures;
using Sdk.Api.Dto;
using Xunit;

namespace Accounts.Tests.Controllers
{
    public class WrongAccount
    {
        public double Balance { get; set; }
        public string ProfileId { get; set; }
    }

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
        public async Task ShouldTurnDownIfBalanceInvalid()
        {
            // Arrange
            var newAccountRequest = new WrongAccount
            {
                ProfileId = Guid.NewGuid().ToString(),
                Balance = double.MaxValue
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