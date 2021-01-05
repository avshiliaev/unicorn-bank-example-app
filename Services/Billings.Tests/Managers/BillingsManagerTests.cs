using System;
using System.Collections.Generic;
using System.Globalization;
using Billings.Interfaces;
using Billings.Managers;
using Billings.Persistence.Entities;
using Billings.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Tests.Extensions;
using Sdk.Tests.Mocks;
using Xunit;

namespace Billings.Tests.Managers
{
    public class BillingsManagerTests
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

        private readonly IBillingsManager _manager;

        public BillingsManagerTests()
        {
            var publishEndpoint = new PublishEndpointMockFactory<ITransactionModel>().GetInstance();
            var billingsRepositoryMock = new RepositoryMockFactory<BillingEntity>(_billingEntities).GetInstance();
            var licenseManagerMock = new LicenseManagerMockFactory<ITransactionModel>().GetInstance();

            _manager = new BillingsManager(
                new Mock<ILogger<BillingsManager>>().Object,
                new BillingsService(billingsRepositoryMock.Object),
                licenseManagerMock.Object,
                publishEndpoint.Object
            );
        }

        [Fact]
        public async void ShouldSuccessfullyCreateANewBilling()
        {
            var transactionCreatedEvent = new TransactionCreatedEvent
            {
                Id = 1.ToGuid().ToString(),
                AccountId = 1.ToGuid().ToString(),
                ProfileId = "awesome",
                Amount = 1,
                Info = "info",
                Approved = false,
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Version = 0
            };
            var newCreatedBilling = await _manager
                .EvaluateTransactionAsync(transactionCreatedEvent);
            Assert.NotNull(newCreatedBilling);
        }

        [Fact]
        public async void ShouldNotCreateAnInvalidBilling()
        {
            var transactionCreatedEvent = new TransactionCreatedEvent();
            var newCreatedBilling = await _manager
                .EvaluateTransactionAsync(transactionCreatedEvent);
            Assert.Null(newCreatedBilling);
        }
    }
}