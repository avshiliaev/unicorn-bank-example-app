using System.Threading.Tasks;
using Billings.Mappers;
using Billings.Persistence.Entities;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Events.Local;
using Sdk.Api.Validators;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Billings.Managers
{
    public class EventStoreManager : IEventStoreManager<ATransactionsState>
    {
        private readonly IEventStoreService<BillingEntity> _eventStoreService;
        private readonly IPublishEndpoint _publishEndpoint;

        public EventStoreManager(
            IEventStoreService<BillingEntity> eventStoreService,
            IPublishEndpoint publishEndpoint
        )
        {
            _eventStoreService = eventStoreService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ATransactionsState> SaveStateAndNotifyAsync(ATransactionsState stateModel)
        {
            if (!stateModel.IsValid())
                return null!;

            var stateEntity = await _eventStoreService.CreateRecordAsync(
                stateModel.ToBillingEntity()
            );

            if (stateEntity == null)
                return null!;

            var accountIsCheckedEvent = stateEntity.ToTransactionModel<TransactionIsCheckedEvent>(stateModel);
            await _publishEndpoint.Publish(accountIsCheckedEvent);

            return stateModel;
        }
    }
}