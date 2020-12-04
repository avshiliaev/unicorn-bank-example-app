using System;
using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Handlers;
using MassTransit.Testing;
using Xunit;

namespace Accounts.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task ShouldConsumeAccountDtoMessage()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new AccountDto
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<AccountDto>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<AccountDto>());

                // the consumer publish the event
                // Assert.True(await harness.Published.Any<AccountDto>());

                // ensure that no faults were published by the consumer
                // Assert.False(await harness.Published.Any<Fault<AccountDto>>());
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task ShouldConsumeTransactionDtoMessage()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TransactionDto());

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<TransactionDto>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<TransactionDto>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}