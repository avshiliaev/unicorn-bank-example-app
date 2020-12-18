using System.Linq;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;
using Sdk.Tests.Mocks;

namespace Sdk.Tests.Extensions
{
    public static class ConfigureTestMessageBus
    {
        public static IServiceCollection AddTestMessageBus<TModel>(this IServiceCollection services)
            where TModel : class, IDataModel
        {
            services.Remove(services.SingleOrDefault(
                    d => d.ServiceType == typeof(IPublishEndpoint)
                )
            );
            services.AddTransient(
                provider => new PublishEndpointMockFactory<TModel>().GetInstance().Object
            );
            return services;
        }
    }
}