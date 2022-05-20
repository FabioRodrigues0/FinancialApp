#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# 6.0-bullseye-slim
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base 
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuyRequest/BuyRequest.Api/BuyRequest.Api.csproj", "BuyRequest/BuyRequest.Api/"]
COPY ["Infrastructure/Infrastructure.Shared/Infrastructure.Shared.csproj", "Infrastructure/Infrastructure.Shared/"]
COPY ["BuyRequest/BuyRequest.Application/BuyRequest.Application.csproj", "BuyRequest/BuyRequest.Application/"]
COPY ["BuyRequest/BuyRequest.Domain/BuyRequest.Domain.csproj", "BuyRequest/BuyRequest.Domain/"]
COPY ["CashBook/CashBook.ApiClient/CashBook.ApiClient.csproj", "CashBook/CashBook.ApiClient/"]
COPY ["CashBook/CashBook.Application/CashBook.Application.csproj", "CashBook/CashBook.Application/"]
COPY ["CashBook/CashBook.Domain/CashBook.Domain.csproj", "CashBook/CashBook.Domain/"]
COPY ["CashBook/CashBook.Data/CashBook.Data.csproj", "CashBook/CashBook.Data/"]
COPY ["BuyRequest/BuyRequest.Data/BuyRequest.Data.csproj", "BuyRequest/BuyRequest.Data/"]
RUN dotnet restore "BuyRequest/BuyRequest.Api/BuyRequest.Api.csproj"
COPY . .
WORKDIR "/src/BuyRequest/BuyRequest.Api"
RUN dotnet build "BuyRequest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuyRequest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "BuyRequest.Api.dll"]