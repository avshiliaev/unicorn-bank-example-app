using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;
using Sdk.License.Abstractions;
using Sdk.License.Interfaces;

namespace Sdk.License.Extensions
{
    public static class ConfigureLicenseManager
    {
        public static IServiceCollection AddLicenseManager<TManager, TModel>(this IServiceCollection services)
            where TModel: class, IDataModel
            where TManager: class, ILicenseManager<TModel>
        {
            services.AddTransient<ILicenseManager<TModel>, TManager>();
            return services;
        } 
    }
}