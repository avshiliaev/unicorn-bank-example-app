using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Notifications.Interfaces;
using Notifications.Mappers;
using Sdk.Api.Dto;
using Sdk.Auth.Extensions;

// https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0
namespace Notifications.Hubs
{
    [Authorize("read:notifications")]
    public class NotificationsHub : Hub
    {
        private readonly INotificationsService _notificationsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationsHub(
            INotificationsService notificationsService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _notificationsService = notificationsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Request()
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            var notifications = _notificationsService.GetAll(profileId);
            var notificationsDto = notifications
                .Select(n => n.ToNotificationsModel<NotificationDto>())
                .ToList();
            await Clients.All.SendAsync("Response", notificationsDto);

            var enumerator = _notificationsService.SubscribeToChanges(profileId);
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