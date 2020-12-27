using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Transactions.Persistence.Entities;

namespace Transactions.Interfaces
{
    public interface ITransactionsService
    {
        Task<IEnumerable<TransactionEntity?>> GetLastTransactionNumber(
            Expression<Func<TransactionEntity?, bool>> predicate);
        Task<TransactionEntity?> CreateTransactionAsync(TransactionEntity transactionEntity);

        Task<TransactionEntity?> GetTransactionByIdAsync(Guid transactionID);

        Task<TransactionEntity?> UpdateTransactionAsync(TransactionEntity transactionEntity);
    }
}