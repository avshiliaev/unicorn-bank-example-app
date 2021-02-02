using Accounts.Persistence.Entities;
using Accounts.Services;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Persistence.Interfaces;

namespace Accounts.Extensions
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