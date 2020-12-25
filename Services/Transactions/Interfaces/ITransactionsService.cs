using System;
using System.Threading.Tasks;
using Transactions.Persistence.Entities;

namespace Transactions.Interfaces
{
    public interface ITransactionsService
    {
        Task<TransactionEntity?> CreateTransactionAsync(TransactionEntity transactionEntity);

        Task<TransactionEntity?> GetTransactionByIdAsync(Guid transactionID);

        Task<TransactionEntity?> UpdateTransactionAsync(TransactionEntity transactionEntity);
    }
}