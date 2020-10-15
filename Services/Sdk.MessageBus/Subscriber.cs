﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Sdk.MessageBus
{
    public class Subscriber
    {
        public static void Main()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    "hello",
                    false,
                    false,
                    false,
                    null
                );

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(
                    "hello",
                    true,
                    consumer
                );

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}