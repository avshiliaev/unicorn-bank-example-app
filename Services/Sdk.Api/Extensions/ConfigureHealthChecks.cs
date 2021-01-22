using Microsoft.Extensions.DependencyInjection;

namespace Sdk.Api.Extensions
{
    public static class ConfigureHealthChecks
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            return services;
        }
    }
}