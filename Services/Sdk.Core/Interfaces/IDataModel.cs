namespace Sdk.Interfaces
{
    public interface IDataModel
    {
        public string EntityId { get; set; }
        public string ProfileId { get; set; }
        public int Version { get; set; }
    }
}