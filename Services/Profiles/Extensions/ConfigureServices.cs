using Microsoft.Extensions.DependencyInjection;
using Profiles.Interfaces;
using Profiles.Services;

namespace Profiles.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IProfilesService, ProfilesService>();
            return services;
        }
    }
}