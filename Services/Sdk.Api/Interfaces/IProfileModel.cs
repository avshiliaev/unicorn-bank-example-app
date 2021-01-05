using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface IProfileModel: IDataModel
    {
        public string Id { get; set; }
    }
}