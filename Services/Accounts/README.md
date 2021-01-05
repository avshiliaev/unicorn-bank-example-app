cd <Project>.Persistence

dotnet ef migrations add Init --context AccountsContext -p ../Accounts.Persistence
dotnet ef migrations add Init --context ApprovalsContext -p ../Approvals.Persistence
dotnet ef migrations add Init --context BillingsContext -p ../Billings.Persistence
dotnet ef migrations add Init --context TransactionsContext -p ../Transactions.Persistence

dotnet ef database update