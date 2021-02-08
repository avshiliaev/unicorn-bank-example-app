using Microsoft.Extensions.DependencyInjection;
using Sdk.Interfaces;

namespace Sdk.Extensions
{
    public static class ConfigureLicenseService
    {
        public static IServiceCollection AddLicenseService<TService, TModel>(this IServiceCollection services)
            where TModel : class, IDataModel
            where TService : class, ILicenseService<TModel>
        {
            services.AddTransient<ILicenseService<TModel>, TService>();
            return services;
        }
    }
}