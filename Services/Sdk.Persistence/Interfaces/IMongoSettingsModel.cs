namespace Sdk.Persistence.Interfaces
{
    public interface IMongoSettingsModel
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}