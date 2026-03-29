# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
WORKDIR /src/BookStoreApp

RUN dotnet restore BookStoreApp.csproj
RUN dotnet publish BookStoreApp.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BookStoreApp.dll"]