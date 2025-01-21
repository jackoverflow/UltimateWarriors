namespace UltimateWarriors.Server.Models
{
    public class WarriorWithWeaponsDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required List<int> WeaponIds { get; set; } // List of weapon IDs
    }

    public class CreateWarriorWithWeaponsDto
    {
        public required string Name { get; set; } // Warrior's name
        public required string Description { get; set; } // Warrior's description
        public required List<int> WeaponIds { get; set; } // List of weapon IDs to associate with the warrior
    }
}