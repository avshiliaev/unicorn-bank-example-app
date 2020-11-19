using Accounts.Communication.Interfaces;
using Accounts.Communication.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Communication.Extensions
{
    public static class MessageBusConfigure
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageBusSubscribeService>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(
                        "event-listener",
                        e => { e.ConfigureConsumer<MessageBusSubscribeService>(context); });
                });
            });

            services.AddMassTransitHostedService();

            services.AddTransient<IMessageBusPublishService, MessageBusPublishService>();

            return services;
        }
    }
}