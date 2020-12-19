using Approvals.Interfaces;
using Approvals.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Approvals.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IApprovalsService, ApprovalsService>();
            return services;
        }
    }
}