# SportOdds

This is a proof of concept for consuming a feed, persisting the data in a db and broadcast the differences to a web client

### Prerequisites
.NET Core 2.2 - https://dotnet.microsoft.com/download/dotnet-core/2.2
node.js - Self host our web client

### Tech
* Server: .NET Core, SignalR, Hangfire, Automapper, Entity Framework Core SQLite
* Database: SQLite
* GUI: ReactJS, signalr, Bootstrap, Webpack, Babel

# Start the server
### Setup
Open package manager console in Visual Studio
Create the db by runnig EF code first migrations:
```sh
Add-Migration -Project SO.Server.Data -StartupProject SO.Server.Data -Name CreateDb 
Update-Database -Project SO.Server.Data -StartupProject SO.Server.Data
```

Starting the server:
You can do that either by starting it from Visual Studio or from cmd:
```sh
$ cd SO.Server.FeedConsumer
$ dotnet build
$ dotnet run
```

At this point we started getting data from the feed and boradcast it to any websockets subscriber

Opening the client GUI

```sh
$ cd BM.WebGui
$ npm install
$ npm start
```
Open link: http://localhost:3000/


