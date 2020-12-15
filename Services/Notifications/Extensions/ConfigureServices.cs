using Microsoft.Extensions.DependencyInjection;
using Notifications.Interfaces;
using Notifications.Services;

namespace Notifications.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<INotificationsService, NotificationsService>();
            return services;
        }
    }
}