using System;

namespace Sdk.Persistence.Interfaces
{
    public interface IMongoEntity
    {
        public string Id { set; get; }
        public string ProfileId { set; get; }
        public string AccountId { set; get; }
        public DateTime Created { set; get; }
        public DateTime Updated { set; get; }
        public int Version { get; set; }
    }
}