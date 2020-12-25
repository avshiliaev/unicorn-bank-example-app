using System.Threading.Tasks;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Transactions.Interfaces
{
    public interface ITransactionsManager
    {
        Task<TransactionDto?> CreateNewTransactionAsync(string profileId);
        Task<TransactionDto?> UpdateStatusOfTransactionAsync(ITransactionModel transactionModel);
    }
}