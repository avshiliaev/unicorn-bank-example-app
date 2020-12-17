using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sdk.Communication.Extensions
{
    public static class MessageBusConfigure
    {
        public static IServiceCollection AddMessageBus<TC>(
            this IServiceCollection services,
            IConfiguration configuration
        )
            where TC : class, IConsumer
        {
            var messageBusSettings = new MessageBusSettingsModel();
            configuration.GetSection("MessageBus").Bind(messageBusSettings);
            var queueName = messageBusSettings.QueueName ?? throw new ArgumentNullException(
                typeof(MessageBusSettingsModel).ToString()
            );
            
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

    public class MessageBusSettingsModel
    {
        public string QueueName { get; set; }
    }
}