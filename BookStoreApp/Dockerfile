FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

# 👉 Go inside project folder
WORKDIR /app/BookStoreApp

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/BookStoreApp/out .

EXPOSE 10000
ENTRYPOINT ["dotnet", "BookStoreApp.dll"]