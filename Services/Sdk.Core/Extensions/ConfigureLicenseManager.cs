using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;

namespace Sdk.Extensions
{
    public static class ConfigureLicenseManager
    {
        public static IServiceCollection AddLicenseManager<TManager, TModel>(this IServiceCollection services)
            where TModel : class, IDataModel
            where TManager : class, ILicenseManager<TModel>
        {
            services.AddTransient<ILicenseManager<TModel>, TManager>();
            return services;
        }
    }
}