using System;
using System.Threading.Tasks;
using MassTransit.Testing;
using Sdk.Api.Events.Domain;
using Transactions.Handlers;
using Xunit;

namespace Transactions.Tests.Handlers
{
    public class SubscriptionsHandlerTests
    {
        [Fact]
        public async Task Should_Consume_TransactionIsCheckedEvent()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<TransactionsSubscriptionsHandler>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send(new TransactionProcessedEvent
                {
                    ProfileId = Guid.NewGuid().ToString()
                });

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