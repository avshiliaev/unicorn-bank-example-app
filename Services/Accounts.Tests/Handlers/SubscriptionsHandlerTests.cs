using System;
using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Handlers;
using MassTransit.Testing;
using Sdk.Api.Events;
using Xunit;

namespace Accounts.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task ShouldConsumeAccountApprovedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new AccountApprovedEvent
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<AccountApprovedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<AccountApprovedEvent>());

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
        public async Task ShouldConsumeTransactionCreatedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<SubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TransactionCreatedEvent());

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<TransactionCreatedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<TransactionCreatedEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}