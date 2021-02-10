using System;
using System.Threading.Tasks;
using Approvals.Handlers;
using MassTransit.Testing;
using Sdk.Api.Events;
using Sdk.Api.Events.Domain;
using Xunit;

namespace Approvals.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task Should_Consume_AccountCreatedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<ApprovalsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new AccountCreatedEvent
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<AccountCreatedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<AccountCreatedEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task Should_Consume_AccountUpdatedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<ApprovalsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new AccountUpdatedEvent
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<AccountUpdatedEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<AccountUpdatedEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}