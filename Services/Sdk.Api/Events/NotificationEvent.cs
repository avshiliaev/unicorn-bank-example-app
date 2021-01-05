using System;
using Sdk.Api.Interfaces;

namespace Sdk.Api.Events
{
    public class NotificationEvent : INotificationModel
    {
        public string Description { get; set; }
        public string ProfileId { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}