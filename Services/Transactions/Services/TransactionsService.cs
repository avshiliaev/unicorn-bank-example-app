using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sdk.Persistence.Interfaces;
using Transactions.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IRepository<TransactionEntity> _transactionRepository;

        public TransactionsService(IRepository<TransactionEntity> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Task<IEnumerable<TransactionEntity?>> GetLastTransactionNumber(
            Expression<Func<TransactionEntity?, bool>> predicate
            )
        {
            return _transactionRepository.GetManyByParameterAsync(predicate!)!;
        }

        public Task<TransactionEntity?> CreateTransactionAsync(TransactionEntity transactionEntity)
        {
            return _transactionRepository.AddAsync(transactionEntity)!;
        }

        public Task<TransactionEntity?> GetTransactionByIdAsync(Guid transactionId)
        {
            return _transactionRepository.GetByIdAsync(transactionId)!;
        }

        public Task<TransactionEntity?> UpdateTransactionAsync(TransactionEntity transactionEntity)
        {
            return _transactionRepository.UpdateActivelyAsync(transactionEntity)!;
        }
    }
}