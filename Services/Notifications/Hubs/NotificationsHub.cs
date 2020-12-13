using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Notifications.Interfaces;

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
            await Clients.All.SendAsync("Response", notifications);
            
            _notificationsService.SubscribeToChanges(profileId);

            return true;
        }
    }
}