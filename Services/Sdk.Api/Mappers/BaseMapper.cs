namespace Sdk.Api.Mappers
{
    public class BaseMapper<TModel, TViewModel> : AutoMapper.Profile
    {
        protected BaseMapper()
        {
            CreateMap<TModel, TViewModel>();
            CreateMap<TViewModel, TModel>();
        }
    }
}