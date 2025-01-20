# UltimateWarriors Repository Documentation

## Overview
The `UltimateWarriorsRepository` class implements data access operations for Warriors, Weapons, and their associations using Dapper with PostgreSQL.

## Interface
```csharp
public interface IUltimateWarriorsRepository
{
    Task<IEnumerable<Warrior>> GetAllWarriors();
    Task<IEnumerable<Weapon>> GetAllWeapons();
    Task<Warrior> CreateWarrior(Warrior warrior);
    Task<Weapon> CreateWeapon(Weapon weapon);
    Task<Warrior> GetWarriorById(int id);
    Task DeleteWarrior(int id);
    Task<WarriorWeapon> AssociateWarriorWithWeapon(WarriorWeapon warriorWeapon);
}
```

## Methods

### GetAllWarriors
```csharp
public async Task<IEnumerable<Warrior>> GetAllWarriors()
```
Retrieves all warriors from the database.
- Returns: A collection of Warrior objects
- SQL: Selects Id, Name, and Description from Warriors table

### GetAllWeapons
```csharp
public async Task<IEnumerable<Weapon>> GetAllWeapons()
```
Retrieves all weapons from the database.
- Returns: A collection of Weapon objects
- SQL: Selects Id, Name, and Description from Weapons table

### CreateWarrior
```csharp
public async Task<Warrior> CreateWarrior(Warrior warrior)
```
Creates a new warrior in the database.
- Parameters: Warrior object with Name and Description
- Returns: Created Warrior with assigned Id
- SQL: Inserts into Warriors table and returns created record

### CreateWeapon
```csharp
public async Task<Weapon> CreateWeapon(Weapon weapon)
```
Creates a new weapon in the database.
- Parameters: Weapon object with Name and Description
- Returns: Created Weapon with assigned Id
- SQL: Inserts into Weapons table and returns created record

### GetWarriorById
```csharp
public async Task<Warrior> GetWarriorById(int id)
```
Retrieves a specific warrior by their Id.
- Parameters: warrior Id
- Returns: Warrior object if found, null if not found
- SQL: Selects warrior by Id from Warriors table

### DeleteWarrior
```csharp
public async Task DeleteWarrior(int id)
```
Deletes a warrior from the database.
- Parameters: warrior Id
- SQL: Deletes warrior from Warriors table

### AssociateWarriorWithWeapon
```csharp
public async Task<WarriorWeapon> AssociateWarriorWithWeapon(WarriorWeapon warriorWeapon)
```
Creates an association between a warrior and a weapon.
- Parameters: WarriorWeapon object containing WarriorId and WeaponId
- Returns: Created association
- SQL: Inserts into WarriorWeapon join table

## Dependencies
- Dapper: For database operations
- Npgsql: PostgreSQL database provider
- Microsoft.Extensions.Configuration: For connection string management
- UltimateWarriors.Server.Models: Contains entity models

## Configuration
The repository requires a connection string named "DefaultConnection" in the application configuration.

## Usage Example
```csharp
// Inject the repository
private readonly IUltimateWarriorsRepository _repository;

public MyController(IUltimateWarriorsRepository repository)
{
    _repository = repository;
}

// Use the repository
public async Task<IActionResult> GetWarriors()
{
    var warriors = await _repository.GetAllWarriors();
    return Ok(warriors);
}
```

