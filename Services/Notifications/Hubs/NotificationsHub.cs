using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Notifications.Interfaces;
using Notifications.Mappers;
using Sdk.Api.Dto;

// https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0
namespace Notifications.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsHub(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        public async Task<bool> Request(string profileId)
        {
            // TODO make limited to a profile!
            var notifications = _notificationsService.GetAll();
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