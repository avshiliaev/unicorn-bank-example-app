using Billings.Interfaces;
using Billings.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Billings.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<IBillingsService, BillingsService>();
            return services;
        }
    }
}