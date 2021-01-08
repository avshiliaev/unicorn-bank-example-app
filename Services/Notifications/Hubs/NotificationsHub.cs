using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Notifications.Interfaces;
using Notifications.Mappers;
using Sdk.Api.Dto;
using Sdk.Auth.Extensions;
using Sdk.Persistence.Extensions;

// https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0
namespace Notifications.Hubs
{
    [Authorize("read:notifications")]
    public class NotificationsHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationsService _notificationsService;

        public NotificationsHub(
            INotificationsService notificationsService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _notificationsService = notificationsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RequestAll()
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            var notifications = _notificationsService.GetManyByParameter(
                e => e.ProfileId == profileId
            );
            var notificationsDto = notifications
                .Select(n => n.ToNotificationsModel<NotificationDto>())
                .ToList();
            await Clients.All.SendAsync("Response", notificationsDto);

            var pipeline = profileId.ToMongoPipelineMatchMany();
            var enumerator = _notificationsService.SubscribeToChanges(pipeline);
            while (enumerator.MoveNext())
                if (enumerator.Current != null)
                    await Clients.All.SendAsync(
                        "Response",
                        enumerator.Current.FullDocument.ToNotificationsModel<NotificationDto>()
                    );
            return true;
        }
    }
}