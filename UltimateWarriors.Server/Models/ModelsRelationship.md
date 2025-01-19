# Models Relationship Documentation

## Warriors
- **Description**: Represents a warrior in the system.
- **Properties**:
  - `Id`: Unique identifier for the warrior.
  - `Name`: Name of the warrior.
  - `Description`: Description of the warrior.
- **Relationships**:
  - **Many-to-Many with Weapons**: 
    - A warrior can have multiple weapons.
    - This relationship is represented through the `WarriorWeapons` collection, which holds instances of the `WarriorWeapon` join entity.

## Weapons
- **Description**: Represents a weapon in the system.
- **Properties**:
  - `Id`: Unique identifier for the weapon.
  - `Name`: Name of the weapon.
  - `Description`: Description of the weapon.
- **Relationships**:
  - **Many-to-Many with Warriors**: 
    - A weapon can be associated with multiple warriors.
    - This relationship is represented through the `WarriorWeapons` collection, which holds instances of the `WarriorWeapon` join entity.

## WarriorWeapon
- **Description**: Represents the join entity that facilitates the many-to-many relationship between `Warriors` and `Weapons`.
- **Properties**:
  - `WarriorId`: Foreign key referencing the `Warriors` model.
  - `Warrior`: Navigation property to the associated `Warriors` instance.
  - `WeaponId`: Foreign key referencing the `Weapons` model.
  - `Weapon`: Navigation property to the associated `Weapons` instance.
