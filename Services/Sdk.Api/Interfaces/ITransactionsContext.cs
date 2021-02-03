using System;
using System.Threading.Tasks;
using Sdk.Api.Abstractions;

namespace Sdk.Api.Interfaces
{
    public interface ITransactionsContext : ITransactionModel
    {
        ITransactionsContext InitializeState(ATransactionsState state, ITransactionModel transactionModel);
        Type GetCurrentState();
        void CheckBlocked();
        void CheckDenied();
        void CheckApproved();
        Task CheckLicense();
        Task PreserveState();
    }
}