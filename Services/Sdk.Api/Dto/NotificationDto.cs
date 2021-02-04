using System;
using Sdk.Api.Interfaces;

namespace Sdk.Api.Dto
{
    public class NotificationDto : INotificationModel
    {
        public string Id { get; set; }
        public string EntityId { get; set; } = null!;
        public int Version { get; set; }
        public string Description { get; set; } = null!;
        public string ProfileId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; } = null!;
    }
}