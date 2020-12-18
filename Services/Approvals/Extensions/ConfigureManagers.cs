using Approvals.Interfaces;
using Approvals.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Approvals.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<IApprovalsManager, ApprovalsManager>();
            return services;
        }
    }
}