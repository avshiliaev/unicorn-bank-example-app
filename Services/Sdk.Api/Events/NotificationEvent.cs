using System;
using Sdk.Interfaces;

namespace Sdk.Api.Events
{
    public class NotificationEvent : INotificationModel
    {
        public string Id { get; set; }
        public string EntityId { get; set; }
        public int Version { get; set; }
        public string? ProfileId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
    }
}