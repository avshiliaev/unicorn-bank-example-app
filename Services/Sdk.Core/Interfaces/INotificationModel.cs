using System;

namespace Sdk.Interfaces
{
    public interface INotificationModel : IDataModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
    }
}