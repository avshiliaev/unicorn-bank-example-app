using Accounts.Interfaces;
using Accounts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Extensions
{
    public static class MessageBusConfigure
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services)
        {
            services.AddTransient<IMessageBusService, MessageBusService>();

            return services;
        }
    }
}