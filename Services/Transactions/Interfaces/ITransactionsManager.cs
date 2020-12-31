using System.Threading.Tasks;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Transactions.Interfaces
{
    public interface ITransactionsManager
    {
        Task<TransactionDto?> CreateNewTransactionAsync(ITransactionModel transactionModel);
        Task<TransactionDto?> ProcessTransactionCheckedEventAsync(ITransactionModel transactionModel);
        Task<IAccountModel?> ProcessAccountUpdatedEventAsync(IAccountModel transactionModel);
    }
}