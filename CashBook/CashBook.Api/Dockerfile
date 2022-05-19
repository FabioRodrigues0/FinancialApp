#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashBook/CashBook.Api/CashBook.Api.csproj", "CashBook/CashBook.Api/"]
COPY ["Infrastructure/Infrastructure.Shared/Infrastructure.Shared.csproj", "Infrastructure/Infrastructure.Shared/"]
COPY ["CashBook/CashBook.Application/CashBook.Application.csproj", "CashBook/CashBook.Application/"]
COPY ["CashBook/CashBook.Domain/CashBook.Domain.csproj", "CashBook/CashBook.Domain/"]
COPY ["CashBook/CashBook.Data/CashBook.Data.csproj", "CashBook/CashBook.Data/"]
RUN dotnet restore "CashBook/CashBook.Api/CashBook.Api.csproj"
COPY . .
WORKDIR "/src/CashBook/CashBook.Api"
RUN dotnet build "CashBook.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashBook.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashBook.Api.dll"]