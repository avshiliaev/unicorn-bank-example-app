using System;
using System.Threading.Tasks;
using MassTransit.Testing;
using Notifications.Handlers;
using Sdk.Api.Events;
using Xunit;

namespace Notifications.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task ShouldConsumeAccountApprovedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<NotificationsSubscriptionsHandler>();

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
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task ShouldConsumeTransactionProcessedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<NotificationsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TransactionProcessedEvent());

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<TransactionProcessedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<TransactionProcessedEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}