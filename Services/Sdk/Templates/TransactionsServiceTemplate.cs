using System.Threading.Tasks;
using Grpc.Core;
using UnicornBank.Sdk.ProtoBuffers;

namespace Sdk.Integrations
{
    public abstract class TransactionsServiceTemplate : Transactions.TransactionsBase
    {
        public abstract override Task<TransactionEvent> Create(TransactionEvent request, ServerCallContext context);
    }
}