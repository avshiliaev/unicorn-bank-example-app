using System;
using Accounts.Persistence.Models;
using Accounts.States.Account;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Mappers;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.Mappers
{
    public class MappingProfile : BaseMapper<AccountRecord, IAccountModel>
    {
        public MappingProfile()
        {
            // Mapping inheritance
            // Runtime polymorphism
            
            // State to record
            CreateMap<AccountPending, AccountRecord>();
            CreateMap<AccountApproved, AccountRecord>();
            CreateMap<AccountDenied, AccountRecord>();
            CreateMap<AccountBlocked, AccountRecord>();
            CreateMap<AAccountState, IRecord>()
                .Include<AccountPending, AccountRecord>()
                .Include<AccountApproved, AccountRecord>()
                .Include<AccountDenied, AccountRecord>()
                .Include<AccountBlocked, AccountRecord>();
            
            // State to event
            CreateMap<AccountPending, AccountCreatedEvent>();
            CreateMap<AccountApproved, AccountUpdatedEvent>();
            CreateMap<AAccountState, IEvent>()
                .Include<AccountPending, AccountCreatedEvent>()
                .Include<AccountApproved, AccountUpdatedEvent>();
            
            // State to dto
            CreateMap<AccountPending, AccountDto>();
            CreateMap<AccountApproved, AccountDto>();
            CreateMap<AccountDenied, AccountDto>();
            CreateMap<AccountBlocked, AccountDto>();
            CreateMap<AAccountState, IAccountModel>()
                .Include<AccountPending, AccountDto>()
                .Include<AccountApproved, AccountDto>()
                .Include<AccountDenied, AccountDto>()
                .Include<AccountBlocked, AccountDto>();
            
            // Record to state
            CreateMap<AccountRecord, AccountPending>();
            CreateMap<AccountRecord, AccountApproved>();
            CreateMap<AccountRecord, AccountDenied>();
            CreateMap<AccountRecord, AccountBlocked>();
            CreateMap<IRecord, AAccountState>()
                .Include<AccountRecord, AccountPending>()
                .Include<AccountRecord, AccountApproved>()
                .Include<AccountRecord, AccountDenied>()
                .Include<AccountRecord, AccountBlocked>();
        }
    }

    public static class NotificationMapper
    {
        public static NotificationEvent ToNotificationEvent(this AccountRecord accountEntity)
        {
            return new NotificationEvent
            {
                Description = $"Your account has been {(accountEntity.Approved ? "approved" : "declined")}.",
                ProfileId = accountEntity.ProfileId,
                Status = accountEntity.Approved ? "approved" : "declined",
                TimeStamp = DateTime.Now,
                Title = $"{(accountEntity.Approved ? "Approval" : "Denial")}",
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}