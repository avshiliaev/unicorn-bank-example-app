using Accounts.Communication.Interfaces;
using Accounts.Communication.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Communication.Extensions
{
    public static class MessageBusConfigure
    {
        public static IServiceCollection AddMessageBus<TC>(
            this IServiceCollection services
        )
            where TC : AMessageBusSubscribeService
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TC>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(
                        "event-listener",
                        e => { e.ConfigureConsumer<TC>(context); });
                });
            });
            services.AddMassTransitHostedService();
            services.AddTransient<IMessageBusPublishService, MessageBusPublishService>();

            return services;
        }
    }
}