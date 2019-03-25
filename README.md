# SportOdds

This is a proof of concept for consuming a feed, persisting the data in a db and broadcast the differences to a web client

### Prerequisites
* C# >= 7.0
* .NET Core >= 2.2 - https://dotnet.microsoft.com/download/dotnet-core/2.2
* node.js - Self host our web client

### Tech
* Server: .NET Core, SignalR, Hangfire, Automapper, Entity Framework Core SQLite
* Database: SQLite
* GUI: ReactJS, signalr, Bootstrap, Webpack, Babel

# Start the server
### Setup
Open package manager console in Visual Studio
Create the db by runnig EF code first migrations:
```sh
Add-Migration -Project SO.Data -StartupProject SO.Data -Name CreateDb 
Update-Database -Project SO.Data -StartupProject SO.Data
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

###Known issues
* Db sync not working - in order to work properly the update object and all its children need to have theier FK set. Currently when we get the root object (Sport) EF does not set FK in the nested collections

###Limitations
SQLite has poor perfomance in multithreded apps.
SQLite only supports a single writer at a time.
SQLite locks the entire database when it needs a lock (either read or write) and only one writer can hold a write lock at a time.
The SQLite provider has some migrations limitations, and mostly these limitations are not EF Core specific but underlying SQLite database engine.
The SQLite provider does not support schemas and Sequences.
The SQLite database engine does not support the following schema operations that are supported by the majority of other relational databases.
* AddForeignKey
* AddPrimaryKey
* AddUniqueConstraint
* AlterColumn
* DropColumn
* DropForeignKey
* DropPrimaryKey
* DropUniqueConstrain
* RenameColumn

sources: https://entityframeworkcore.com/providers-sqlite
https://stackoverflow.com/questions/4539542/sqlite-vs-sql-server

