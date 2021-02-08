using System.Threading.Tasks;
using Accounts.States.Account;
using Accounts.States.Transactions;
using AutoMapper;
using Sdk.Api.Dto;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;
using Sdk.StateMachine.Interfaces;

namespace Accounts.Managers
{
    public class StatesManager : IStatesManager
    {
        private readonly IAccountContext _accountContext;
        private readonly IEventStoreService<AAccountState> _eventStoreService;
        private readonly ILicenseService<AAccountState> _licenseService;
        private readonly IMapper _mapper;
        private readonly IPublishService<AAccountState> _publishService;
        private readonly ITransactionsContext _transactionsContext;

        public StatesManager(
            IAccountContext accountContext,
            ITransactionsContext transactionsContext,
            IMapper mapper,
            IEventStoreService<AAccountState> eventStoreService,
            ILicenseService<AAccountState> licenseService,
            IPublishService<AAccountState> publishService
        )
        {
            _accountContext = accountContext;
            _transactionsContext = transactionsContext;
            _mapper = mapper;
            _eventStoreService = eventStoreService;
            _licenseService = licenseService;
            _publishService = publishService;
        }

        // Builder Pattern
        public async Task<IAccountModel> ProcessAccountState(IAccountModel accountModel)
        {
            // State
            _accountContext.InitializeState(new AccountPending(), accountModel);
            // Fluent interface
            _accountContext
                .CheckBlocked()
                .CheckDenied()
                .CheckApproved();

            await _accountContext.CheckLicense(_licenseService);
            await _accountContext.PreserveState(_eventStoreService);
            await _accountContext.PublishEvent(_publishService);

            return _mapper.Map<AccountDto>(_accountContext);
        }

        public async Task<ITransactionModel> ProcessTransactionState(ITransactionModel transactionModel)
        {
            _transactionsContext.InitializeState(new TransactionPending(), transactionModel);
            _transactionsContext
                .CheckBlocked()
                .CheckDenied()
                .CheckApproved();

            // await _transactionsContext.CheckLicense();
            // await _transactionsContext.PreserveState();
            // await _transactionsContext.PublishEvent(_publishService);

            return _mapper.Map<TransactionDto>(_transactionsContext);
        }
    }

    public interface IStatesManager
    {
        public Task<IAccountModel> ProcessAccountState(IAccountModel accountModel);
        public Task<ITransactionModel> ProcessTransactionState(ITransactionModel transactionModel);
    }
}