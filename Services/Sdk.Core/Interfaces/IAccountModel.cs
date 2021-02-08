namespace Sdk.Interfaces
{
    public interface IAccountModel : IDataModel, IApprovable, IConcurrentHost
    {
        public string Id { get; set; }
        public float Balance { get; set; }
    }
}