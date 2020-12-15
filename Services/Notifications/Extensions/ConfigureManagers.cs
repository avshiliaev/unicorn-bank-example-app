using Microsoft.Extensions.DependencyInjection;
using Notifications.Managers;

namespace Notifications.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<INotificationsManager, NotificationsManager>();
            return services;
        }
    }
}