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
        public async Task Should_Consume_NotificationEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<NotificationsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new NotificationEvent
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<NotificationEvent>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<NotificationEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}