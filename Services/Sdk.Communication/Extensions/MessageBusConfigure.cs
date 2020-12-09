using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Communication.Extensions
{
    public static class MessageBusConfigure
    {
        public static IServiceCollection AddMessageBus<TC>(
            this IServiceCollection services,
            string queueName
        )
            where TC : class, IConsumer
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TC>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(
                        queueName,
                        e => { e.ConfigureConsumer<TC>(context); });
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}