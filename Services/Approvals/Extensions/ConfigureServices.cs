using Approvals.Interfaces;
using Approvals.Persistence.Entities;
using Approvals.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Approvals.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreService<ApprovalEntity>, EventStoreService>();
            return services;
        }
    }
}