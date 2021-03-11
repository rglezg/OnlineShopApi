# OnlineShopApi

ASP.NET Core RESTful API sample

## Requirements
- .NET 5 SDK
- MySQL (v5 onwards)

## How to run

1. Set the database connection string in the `src/OnlineShop.APi/appsettings.Development.json` file as well as the MySQL version used.

  ````
  "ConnectionStrings": {
    "MySQL": "Server=localhost;Database=online_shop;Uid=root;Pwd=MySQL*1234;"
  },
  "DbProviders" : {
    "MySQL": {
      "Version" : "5.7.33"
    }
  },
  ````
2. Create the database schema running `update-database.bat` or using the `online_shop.sql` script.

3. Run the project with `dotnet run`. The default swagger address is `https://localhost:5001/swagger/index.html`

## Login Info

| Username | Password    |
| -------- | ----------- |
| admin    | Qwerty*1234 |
| seller   | Asdfg*1234  |
| client   | Zxcvb*1234  |
