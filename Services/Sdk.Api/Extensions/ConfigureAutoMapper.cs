using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Sdk.Api.Extensions
{
    public static class ConfigureAutoMapper
    {
        public static IServiceCollection AddCustomAutoMapper<TProfile>(this IServiceCollection services)
            where TProfile : Profile, new()
        {
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new TProfile()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}