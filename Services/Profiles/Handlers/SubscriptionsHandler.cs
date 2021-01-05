using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Profiles.Interfaces;
using Sdk.Api.Events;

namespace Profiles.Handlers
{
    public class ProfilesSubscriptionsHandler : IConsumer<AccountCreatedEvent>
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

        // TODO: Does ? change the signature?
        public Task? Consume(ConsumeContext<AccountCreatedEvent> context)
        {
            _logger.LogDebug($"Received new ProfileEvent for {context.Message.Id}");
            var profileDto = _profilesManager.AddNewProfile(context.Message);

            if (profileDto != null)
                return Task.CompletedTask;
            return null;
        }
    }
}