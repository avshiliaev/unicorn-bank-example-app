using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Billings.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Billings.Services
{
    public class EventStoreService : IEventStoreService<BillingEntity>
    {
        private readonly IRepository<BillingEntity> _approvalsRepository;

        public EventStoreService(IRepository<BillingEntity> approvalsRepository)
        {
            _approvalsRepository = approvalsRepository;
        }

        public Task<BillingEntity> CreateRecordAsync(BillingEntity approvalEntity)
        {
            return _approvalsRepository.AddAsync(approvalEntity);
        }

        public Task<List<BillingEntity>> GetManyRecordsLastVersionAsync(
            Expression<Func<BillingEntity, bool>> predicate
        )
        {
            return _approvalsRepository.GetManyLastVersionAsync(predicate);
        }

        public Task<BillingEntity> GetOneRecordAsync(Expression<Func<BillingEntity, bool>> predicate)
        {
            return _approvalsRepository.GetOneAsync(predicate);
        }
    }
}