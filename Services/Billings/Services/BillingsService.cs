using System;
using System.Threading.Tasks;
using Billings.Interfaces;
using Billings.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Billings.Services
{
    public class BillingsService : IBillingsService
    {
        private readonly IRepository<BillingEntity> _billingsRepository;

        public BillingsService(IRepository<BillingEntity> billingsRepository)
        {
            _billingsRepository = billingsRepository;
        }

        public async Task<BillingEntity> CreateBillingAsync(BillingEntity billingEntity)
        {
            return await _billingsRepository.AddAsync(billingEntity);
        }

        public Task<BillingEntity> GetBillingByIdAsync(Guid billingId)
        {
            throw new NotImplementedException();
        }

        public Task<BillingEntity> UpdateBillingAsync(BillingEntity billingEntity)
        {
            throw new NotImplementedException();
        }
    }
}