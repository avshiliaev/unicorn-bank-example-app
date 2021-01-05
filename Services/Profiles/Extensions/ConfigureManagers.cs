using Microsoft.Extensions.DependencyInjection;
using Profiles.Interfaces;
using Profiles.Managers;

namespace Profiles.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services.AddTransient<IProfilesManager, ProfilesManager>();
            return services;
        }
    }
}