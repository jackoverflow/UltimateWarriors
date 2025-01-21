public class WarriorWithWeaponsDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<int> WeaponIds { get; set; } // List of weapon IDs
} 