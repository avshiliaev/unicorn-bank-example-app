using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Sdk.Interfaces;
using Sdk.Tests.Interfaces;
using Transactions.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Tests.Mocks
{
    public class ConcurrencyManagerMockFactory : IMockFactory<IConcurrencyManager>
    {
        private readonly List<TransactionEntity> _entities;

        public ConcurrencyManagerMockFactory(List<TransactionEntity> entities)
        {
            _entities = entities;
        }

        public Mock<IConcurrencyManager> GetInstance()
        {
            var licenseManager = new Mock<IConcurrencyManager>();
            licenseManager
                .Setup(
                    p => p.SetNextSequentialNumber(
                        It.IsAny<ITransactionModel>()
                    )
                )
                .Returns((ITransactionModel transactionModel) =>
                {
                    var allTransactions = _entities.Where(
                        entity => entity!.ProfileId == transactionModel.ProfileId
                                  && entity!.AccountId == transactionModel.AccountId.ToGuid()
                    );
                    var lastTransactionNumber = allTransactions.Max(t => t?.SequentialNumber);

                    transactionModel.SequentialNumber = lastTransactionNumber.GetValueOrDefault(0) + 1;
                    return Task.FromResult(transactionModel);
                });
            return licenseManager;
        }
    }
}