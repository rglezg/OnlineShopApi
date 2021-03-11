@echo off
dotnet ef database update -s "src\OnlineShop.Api\OnlineShop.Api.csproj" -p "src\OnlineShop.Data\OnlineShop.Data.csproj" --verbose