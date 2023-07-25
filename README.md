# webapp
Simple web application built by .NET

# Build Database
"Add-Migration InitialCreate" and "Update-Database" are commands used in Entity Framework, a popular Object-Relational Mapping (ORM) framework for .NET applications. These commands are typically executed in the Package Manager Console or the .NET Core CLI when working with Entity Framework in a .NET project.

if you create a new entity class or modify an existing one, you would use this command to generate a migration that captures those changes in a way that Entity Framework can apply to the database
``` 
Add-Migration InitialCreate

```
After running the Add-Migration command and creating a new migration, you need to use Update-Database to apply the changes to the actual database. Once the migration is applied, the changes in your entity classes will be reflected in the database structure
```
Update-Database
```
