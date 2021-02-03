using System.Threading.Tasks;
using Approvals.Mappers;
using Approvals.Persistence.Entities;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Events.Local;
using Sdk.Api.Validators;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Approvals.Managers
{
    public class EventStoreManager : IEventStoreManager<AAccountState>
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

        public async Task<AAccountState> SaveStateAsync(AAccountState stateModel)
        {
            if (!stateModel.IsValid())
                return null!;

            var stateEntity = await _eventStoreService.CreateRecordAsync(
                stateModel.ToApprovalEntity()
            );

            if (stateEntity == null)
                return null!;

            var accountIsCheckedEvent = stateEntity.ToAccountModel<AccountIsCheckedEvent>(stateModel);
            await _publishEndpoint.Publish(accountIsCheckedEvent);

            return stateModel;
        }
    }
}