using System;
using System.Threading.Tasks;
using Billings.Handlers;
using MassTransit.Testing;
using Xunit;

namespace Billings.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task Should_Consume_TransactionCheckCommand()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<BillingsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TransactionCheckCommand
                {
                    Id = Guid.NewGuid().ToString(),
                    ProfileId = Guid.NewGuid().ToString()
                });

                // did the endpoint consume the message
                Assert.True(await harness.Consumed.Any<TransactionCheckCommand>());

                // did the actual consumer consume the message
                Assert.True(await consumerHarness.Consumed.Any<TransactionCheckCommand>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}