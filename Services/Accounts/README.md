cd Accounts.Persistence
dotnet ef migrations add Init --context AccountsContext -p ../Accounts.Persistence
dotnet ef database update