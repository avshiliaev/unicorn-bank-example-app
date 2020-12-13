namespace Sdk.Persistence.Interfaces
{
    public interface IMongoSettingsModel
    {
        string NotificationsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}