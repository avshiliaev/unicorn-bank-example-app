﻿using System.Threading.Tasks;
using Grpc.Core;
using UnicornBankSdk;

namespace Sdk.Integrations
{
    public abstract class AccountsServiceTemplate: Accounts.AccountsBase
    {
        public abstract override Task<AccountEvent> Create(AccountEvent request, ServerCallContext context);
    }
}