using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Approvals.Mappers;
using Approvals.Persistence.Entities;
using MassTransit;
using Sdk.Api.Events.Local;
using Sdk.Api.Validators;

namespace Approvals.Managers
{
    public class EventStoreManager : IEventStoreManager<AbstractAccountState>
    {
        private readonly IEventStoreService<ApprovalEntity> _eventStoreService;
        private readonly IPublishEndpoint _publishEndpoint;

        public EventStoreManager(
            IEventStoreService<ApprovalEntity> eventStoreService,
            IPublishEndpoint publishEndpoint
        )
        {
            _eventStoreService = eventStoreService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<AbstractAccountState?> SaveStateAndNotifyAsync(AbstractAccountState stateModel)
        {
            if (!stateModel.IsValid())
                return null!;

            var stateEntity = await _eventStoreService.CreateApprovalAsync(
                stateModel.ToApprovalEntity()
            );

            if (stateEntity == null)
                return null;

            var accountIsCheckedEvent = stateEntity.ToAccountModel<AccountIsCheckedEvent>(
                stateEntity.ToAccountModel<AccountIsCheckedEvent>(stateModel)
            );
            await _publishEndpoint.Publish(accountIsCheckedEvent);

            return stateModel;
        }
    }
}