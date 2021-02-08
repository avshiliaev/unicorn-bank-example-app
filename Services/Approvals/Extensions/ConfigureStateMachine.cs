using Microsoft.Extensions.DependencyInjection;

namespace Approvals.Extensions
{
    public static class ConfigureStateMachine
    {
        public static IServiceCollection AddStateMachine(this IServiceCollection services)
        {
            services.AddTransient<IAccountContext, AccountContext>();
            return services;
        }
    }
}