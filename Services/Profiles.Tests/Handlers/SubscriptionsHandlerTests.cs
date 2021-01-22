using System;
using System.Threading.Tasks;
using MassTransit.Testing;
using Profiles.Handlers;
using Sdk.Api.Events;
using Xunit;

namespace Profiles.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        
        /*
         *         IConsumer<AccountCreatedEvent>,
        IConsumer<AccountUpdatedEvent>,
        IConsumer<TransactionCreatedEvent>,
        IConsumer<TransactionUpdatedEvent>
         */
        [Fact]
        public async Task Should_Consume_NotificationEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<ProfilesSubscriptionsHandler>();

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
    }
}