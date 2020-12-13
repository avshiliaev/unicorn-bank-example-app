using Notifications.Persistence.Entities;
using Sdk.Persistence.Abstractions;
using Sdk.Persistence.Interfaces;

namespace Notifications.Persistence.Repositories
{
    public class NotificationsRepository : AbstractMongoRepository<NotificationEntity>
    {
        public NotificationsRepository(IMongoSettingsModel settings) : base(settings)
        {
        }
    }
}