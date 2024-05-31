# Entity Framework

This project is responsible for establishing the database connection as well as acting as an Object-Relational Mapper ([O/RM](https://www.prisma.io/dataguide/types/relational/what-is-an-orm)). 

We will be using Entity Framework Core ([EF7](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew)) to handle the data-access logic. 

## EFCore
The main component you will interact with is the DbContext class. You can refer to our own implementation of this component by looking at PDatabaseContext.cs. We define the path to the SQLite database within this class.

The below article goes into detail on how to use Entity Framework Core DbContext.

https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

Furthermore, the entify framework will provide us with command line tools to manage our database.

### Migrations

In real world projects, data models change as features get implemented: new entities or properties are added and removed, and database schemas need to be changed accordingly to be kept in sync with the application. The migrations feature in EF Core provides a way to incrementally update the database schema to keep it in sync with the application's data model while preserving existing data in the database.

At a high level, migrations function in the following way:

- When a data model change is introduced, the developer uses EF Core tools to add a corresponding migration describing the updates necessary to keep the database schema in sync. EF Core compares the current model against a snapshot of the old model to determine the differences, and generates migration source files; the files can be tracked in your project's source control like any other source file.
- Once a new migration has been generated, it can be applied to a database in various ways. EF Core records all applied migrations in a special history table, allowing it to know which migrations have been applied and which haven't.

#### List all your Migrations
The first command you want to run is below. It will look at the Migrations folder and display any migrations it finds. 

```
dotnet ef migrations list
```

#### Create your First Migration
If for some reason you do not have any migrations to list, go ahead and create one with the below command.

```
dotnet ef migrations add CreateInitialTablesAndSeed
```

#### Reverse your Last Migration
We will constantly create and drop tables during the development life cycle. For this reason, you can remove any migrations with the below command.

```
dotnet ef migrations remove
```

### Database

Once you add a migration, you will have to run it for the changes to be reflected in the database. 

#### Apply Latest Migrations
To apply the latest migration changes run this command on your command line.

```
dotnet ef database update
```

#### Undo ALL Migrations
To undo the changes made by the migration run the below command. This is useful when you want to start over with a blank database.

```
dotnet ef database update 0
```

#### !Important
We will only ever have one migration (for now) at any given time. This will be the initial migration that will set up the database. Once we decide on a solid implementation of the database we can start adding additional migrations. For this reason, you can always remove and readd migrations after making changes to entities.