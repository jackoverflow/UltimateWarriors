-- Create table for Warriors
CREATE TABLE Warriors (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT
);

-- Create table for Weapons
CREATE TABLE Weapons (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT
);

-- Create table for WarriorWeapon (join table)
CREATE TABLE WarriorWeapon (
    WarriorId INT NOT NULL,
    WeaponId INT NOT NULL,
    PRIMARY KEY (WarriorId, WeaponId),
    FOREIGN KEY (WarriorId) REFERENCES Warriors(Id) ON DELETE CASCADE,
    FOREIGN KEY (WeaponId) REFERENCES Weapons(Id) ON DELETE CASCADE
);
