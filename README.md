# README

```df``` is a data factory tool: it can be used to produce random data to insert into SQL Server tables.

This application targets SQL Server 2017, as it was the latest version of SQL Server available at the time of development.

```df``` is extensible. Developers may create their own configurable value factories if the ones provided do not meet their needs. Please see the projects ```df.valuefactories``` and ```df.valuefactories.spatial``` for examples.

## Development
This solution has been created using [Visual Studio 2019 Enterprise Edition](https://visualstudio.com). The projects are built with [dotnet core 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0) and the code has been written in [C# 8.0](https://github.com/dotnet/csharplang).

### Visual Studio extensions
* [Intellicode](https://prod.intellicode.vsengsaas.visualstudio.com/get?m=88DA84992B9A4A57A981B27DF936E21D) has been used.

## Dependencies

### LocalDB
The application depends on [LocalDB 2017](https://www.mssqltips.com/sqlservertip/5612/getting-started-with-sql-server-2017-express-localdb/).

### Github repositories
The Xeger code in ```Df.Stochastic.Fare``` has been copied from [Fare](https://github.com/moodmosaic/Fare).

### NuGet Packages
Please run the command ```Get-Package``` in the Package Manager Console in Visual Studio to view and extensive and current list of referenced packages.
