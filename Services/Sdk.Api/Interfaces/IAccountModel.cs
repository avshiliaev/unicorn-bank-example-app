using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface IAccountModel : IDataModel, IApprovable, IConcurrentHost
    {
        public string Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public string AccountId { get; set; }
    }
}