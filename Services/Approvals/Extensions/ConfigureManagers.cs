using Approvals.Interfaces;
using Approvals.Managers;
using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;
using Sdk.License.Abstractions;

namespace Approvals.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services
                .AddTransient<IApprovalsManager, ApprovalsManager>();
            return services;
        }
    }
}