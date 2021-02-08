using AutoMapper;

namespace Sdk.Api.Mappers
{
    public class BaseMapper<TModel, TViewModel> : Profile
    {
        protected BaseMapper()
        {
            CreateMap<TModel, TViewModel>();
            CreateMap<TViewModel, TModel>();
        }
    }
}