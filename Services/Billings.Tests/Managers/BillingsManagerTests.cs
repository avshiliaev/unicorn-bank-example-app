using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Moq;
using Sdk.Api.Events;
using Sdk.Extensions;
using Sdk.Interfaces;
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
        public async void Should_EvaluateTransactionAsync_Valid()
        {
            var transactionCheckCommand = new TransactionCheckCommand
            {
                Id = 1.ToGuid().ToString(),
                AccountId = 1.ToGuid().ToString(),
                ProfileId = "awesome",
                Amount = 1,
                Info = "info",
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Version = 0
            };
            transactionCheckCommand.SetPending();
            var transactionIsCheckedEvent = await _manager.EvaluateTransactionAsync(
                transactionCheckCommand
            );
            Assert.NotNull(transactionIsCheckedEvent);
            Assert.True(transactionIsCheckedEvent.IsApproved());
        }

        [Fact]
        public async void ShouldNot_EvaluateTransactionAsync_Invalid()
        {
            var transactionCheckCommand = new TransactionCreatedEvent();
            transactionCheckCommand.SetPending();
            var transactionIsCheckedEvent = await _manager
                .EvaluateTransactionAsync(transactionCheckCommand);
            Assert.Null(transactionIsCheckedEvent);
        }
    }
}