using System;
using Accounts.Persistence.Entities;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Api.Mappers;

namespace Accounts.Mappers
{
    public class MappingProfile : BaseMapper<AccountEntity, IAccountModel>
    {
        public MappingProfile()
        {
            CreateMap<AccountEntity, IAccountModel>();
            CreateMap<IAccountModel, AccountEntity>();
        }
    }

    public static class NotificationMapper
    {
        public static NotificationEvent ToNotificationEvent(this AccountEntity accountEntity)
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