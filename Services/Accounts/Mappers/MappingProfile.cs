using System;
using Accounts.Persistence.Models;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Api.Mappers;

namespace Accounts.Mappers
{
    public class MappingProfile : BaseMapper<AccountRecord, IAccountModel>
    {
        public MappingProfile()
        {
            CreateMap<AccountRecord, IAccountModel>();
            CreateMap<IAccountModel, AccountRecord>();
        }
    }

    public static class NotificationMapper
    {
        public static NotificationEvent ToNotificationEvent(this AccountRecord accountEntity)
        {
            return new NotificationEvent
            {
                Description = $"Your account has been {(accountEntity.Approved ? "approved" : "declined")}.",
                ProfileId = accountEntity.ProfileId.ToString(),
                Status = accountEntity.Approved ? "approved" : "declined",
                TimeStamp = DateTime.Now,
                Title = $"{(accountEntity.Approved ? "Approval" : "Denial")}",
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}