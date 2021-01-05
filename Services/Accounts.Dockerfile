FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR .
COPY ./ .
# RUN dotnet restore

WORKDIR /Accounts
RUN dotnet publish -c Release -o out --self-contained false --runtime linux-x64 --verbosity quiet

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR .
COPY --from=build /Accounts/out ./
ENTRYPOINT ["dotnet", "Accounts.dll"]
