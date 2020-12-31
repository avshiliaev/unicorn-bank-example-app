using System;
using System.Threading.Tasks;
using Accounts.Handlers;
using MassTransit.Testing;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Xunit;

namespace Accounts.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task ShouldConsumeAccountApprovedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<AccountsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new AccountIsCheckedEvent
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<AccountIsCheckedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<AccountIsCheckedEvent>());
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
            var consumerHarness = harness.Consumer<AccountsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TransactionUpdatedEvent());

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<TransactionUpdatedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<TransactionUpdatedEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}