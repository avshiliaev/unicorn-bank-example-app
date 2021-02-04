using Approvals.Persistence.Entities;
using Approvals.Services;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Persistence.Interfaces;

namespace Approvals.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreService<AccountEntity>, EventStoreService>();
            return services;
        }
    }
}