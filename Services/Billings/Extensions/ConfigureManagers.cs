using Billings.Interfaces;
using Billings.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Billings.Extensions
{
    public static class ConfigureManagers
    {
        public static IServiceCollection AddBusinessLogicManagers(this IServiceCollection services)
        {
            services
                .AddTransient<IBillingsManager, BillingsManager>()
                .AddTransient<ILicenseManager, LicenseManager>();
            return services;
        }
    }
}