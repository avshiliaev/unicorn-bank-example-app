using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        // [Fact]
        public async Task ShouldTurnDownIfNoAccountIdProvided()
        {
            // Arrange
            var requestUrl = "/api/transactions";
            var newTransaction = new TransactionViewModel
            {
                AccountId = "",
                Amount = 1.0f,
                Info = "Info"
            };

            // Act
            var response = await _client.PostAsync(
                requestUrl, 
                new StringContent(JsonSerializer.Serialize(newTransaction), Encoding.UTF8)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
                );

            // Assert
            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode
            );
        }

        // [Fact]
        public async Task CreateNewTransactionRightData()
        {
            // Arrange
            var requestUrl = "/api/transactions";
            var newTransaction = new TransactionViewModel
            {
                AccountId = 1.ToGuid().ToString(),
                Amount = 1.0f,
                Info = "Info"
            };

            // Act
            var response = await _client.PostAsync(
                requestUrl, 
                new StringContent(JsonSerializer.Serialize(newTransaction), Encoding.UTF8)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            );

            // Assert
            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode
            );
        }
    }
}