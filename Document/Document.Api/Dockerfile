#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Document/Document.Api/Document.Api.csproj", "Document/Document.Api/"]
COPY ["Infrastructure/Infrastructure.Shared/Infrastructure.Shared.csproj", "Infrastructure/Infrastructure.Shared/"]
COPY ["Document/Document.Application/Document.Application.csproj", "Document/Document.Application/"]
COPY ["CashBook/CashBook.ApiClient/CashBook.ApiClient.csproj", "CashBook/CashBook.ApiClient/"]
COPY ["CashBook/CashBook.Application/CashBook.Application.csproj", "CashBook/CashBook.Application/"]
COPY ["CashBook/CashBook.Domain/CashBook.Domain.csproj", "CashBook/CashBook.Domain/"]
COPY ["CashBook/CashBook.Data/CashBook.Data.csproj", "CashBook/CashBook.Data/"]
COPY ["Document/Document.Domain/Document.Domain.csproj", "Document/Document.Domain/"]
COPY ["Document/Document.Data/Document.Data.csproj", "Document/Document.Data/"]
RUN dotnet restore "Document/Document.Api/Document.Api.csproj"
COPY . .
WORKDIR "/src/Document/Document.Api"
RUN dotnet build "Document.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Document.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Document.Api.dll"]