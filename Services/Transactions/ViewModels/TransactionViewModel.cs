using System;
using Sdk.Api.Validators;

namespace Transactions.ViewModels
{
    public class TransactionViewModel
    {
        [IsValidGuid] public string AccountId { get; set; }

        public float Amount { get; set; }

        public string Info { get; set; }
    }
}