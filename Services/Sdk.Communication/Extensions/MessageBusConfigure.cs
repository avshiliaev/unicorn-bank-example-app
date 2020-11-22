using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;

namespace Accounts.Communication.Extensions
{
    public static class MessageBusConfigure
    {
        public static IServiceCollection AddMessageBus<TC>(
            this IServiceCollection services
        )
            where TC : class, IConsumer
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

            return services;
        }
    }
}