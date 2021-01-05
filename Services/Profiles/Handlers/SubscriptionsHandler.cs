using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Profiles.Interfaces;
using Sdk.Api.Events;

namespace Profiles.Handlers
{
    public class ProfilesSubscriptionsHandler :
        IConsumer<AccountCreatedEvent>,
        IConsumer<AccountUpdatedEvent>,
        IConsumer<TransactionCreatedEvent>,
        IConsumer<TransactionUpdatedEvent>
    {
        private readonly ILogger<ProfilesSubscriptionsHandler> _logger;
        private readonly IProfilesManager _profilesManager;

        public ProfilesSubscriptionsHandler(
            ILogger<ProfilesSubscriptionsHandler> logger,
            IProfilesManager profilesManager
        )
        {
            _logger = logger;
            _profilesManager = profilesManager;
        }

        public ProfilesSubscriptionsHandler()
        {
        }

        public Task? Consume(ConsumeContext<AccountCreatedEvent> context)
        {
            _logger.LogDebug($"Received new AccountCreatedEvent for {context.Message.Id}");
            var profileDto = _profilesManager.AddNewProfile(context.Message);

            if (profileDto != null)
                return Task.CompletedTask;
            return null;
        }

        public Task? Consume(ConsumeContext<AccountUpdatedEvent> context)
        {
            _logger.LogDebug($"Received new AccountUpdatedEvent for {context.Message.Id}");
            var profileDto = _profilesManager.UpdateProfile(context.Message);

            if (profileDto != null)
                return Task.CompletedTask;
            return null;
        }

        public Task? Consume(ConsumeContext<TransactionCreatedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionCreatedEvent for {context.Message.Id}");
            var profileDto = _profilesManager.AddTransactionToProfile(context.Message);

            if (profileDto != null)
                return Task.CompletedTask;
            return null;
        }

        public Task? Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionUpdatedEvent for {context.Message.Id}");
            var profileDto = _profilesManager.UpdateTransactionOnProfile(context.Message);

            if (profileDto != null)
                return Task.CompletedTask;
            return null;
        }
    }
}