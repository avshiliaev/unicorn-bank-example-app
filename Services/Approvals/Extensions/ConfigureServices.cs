using Approvals.Services;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Approvals.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreService<AAccountState>, EventStoreService>();
            services.AddTransient<ILicenseService<AAccountState>, LicenseService>();
            services.AddTransient<IPublishService<AAccountState>, PublishService>();
            return services;
        }
    }
}