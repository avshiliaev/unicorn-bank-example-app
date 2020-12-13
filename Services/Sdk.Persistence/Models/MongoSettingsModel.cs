using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Models
{
    public class MongoSettingsModel : IMongoSettingsModel
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}