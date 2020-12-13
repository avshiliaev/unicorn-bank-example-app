using System;
using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public class INotificationModel : IDataModel
    {
        public string Description { get; set; }
        public Guid ProfileId { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}