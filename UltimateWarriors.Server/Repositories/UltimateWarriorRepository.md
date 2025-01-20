Retrieves all warriors from the database asynchronously.

#### Returns
- `Task<IEnumerable<Warriors>>`: A collection of Warriors objects

#### Implementation Details
- Uses Dapper for efficient data access
- Connects to PostgreSQL using the configured connection string
- Executes a SELECT query to fetch all warriors

## Dependencies
- Dapper
- Npgsql
- Microsoft.Extensions.Configuration
- UltimateWarriors.Server.Models

## Registration
The repository must be registered in `Program.cs` for dependency injection to work properly. Here's an example:

```csharp
builder.Services.AddScoped<IUltimateWarriorsRepository, UltimateWarriorsRepository>();
``` 

This registration ensures that the `UltimateWarriorsRepository` can be injected into other components using dependency injection.

