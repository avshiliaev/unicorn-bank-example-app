using System;
using System.Collections.Generic;
using Billings.Persistence.Entities;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Billings.Tests.Services
{
    public class BillingsServiceTests
    {
        private readonly List<BillingEntity> _billingEntities = new List<BillingEntity>
        {
            new BillingEntity
            {
                Id = 1.ToGuid(),
                Approved = true,
                TransactionId = 1.ToGuid(),
                Version = 0
            },
            new BillingEntity
            {
                Id = 2.ToGuid(),
                Approved = false,
                TransactionId = 1.ToGuid(),
                Version = 0
            },
            new BillingEntity
            {
                Id = 3.ToGuid(),
                Approved = true,
                TransactionId = 2.ToGuid(),
                Version = 0
            }
        };

        private readonly IBillingsService _service;

        public BillingsServiceTests()
        {
            var billingsRepositoryMock = new RepositoryMockFactory<BillingEntity>(_billingEntities).GetInstance();
            _service = new BillingsService(billingsRepositoryMock.Object);
        }

        [Fact]
        public async void ShouldSuccessfullyCreateANewBilling()
        {
            var newBillingEntity = new BillingEntity
            {
                Id = Guid.NewGuid()
            };
            var newCreatedBillingEntity = await _service.CreateBillingAsync(newBillingEntity);
            Assert.NotNull(newCreatedBillingEntity);
        }
    }
}