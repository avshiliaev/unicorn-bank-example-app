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

        public async Task<bool> Request(string user)
        {
            var notifications = _notificationsService.GetAll();
            await Clients.All.SendAsync("Response", notifications);

            return true;
        }
    }
}