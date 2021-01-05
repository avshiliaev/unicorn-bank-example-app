using System;
using System.Threading.Tasks;
using Approvals.Handlers;
using MassTransit.Testing;
using Sdk.Api.Events.Local;
using Xunit;

namespace Approvals.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task ShouldConsumeAccountCreatedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<ApprovalsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new AccountCheckCommand
                {
                    Id = Guid.NewGuid().ToString(),
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<AccountCheckCommand>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<AccountCheckCommand>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}