using System;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Communication.Models;

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
            var host = messageBusSettings.Host ?? "localhost";
            var username = messageBusSettings.Username ?? "guest";
            var password = messageBusSettings.Password ?? "guest";

            services.AddMassTransit(x =>
            {
                x.AddConsumer<TC>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint(
                        queueName,
                        e =>
                        {
                            e.UseMessageRetry(
                                r => r.Interval(100, TimeSpan.FromSeconds(2))
                            );
                            e.ConfigureConsumer<TC>(context);
                        }
                    );
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}