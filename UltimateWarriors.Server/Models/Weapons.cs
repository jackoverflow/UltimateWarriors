namespace UltimateWarriors.Server.Models {
    public class Weapons
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<WarriorWeapon> WarriorWeapons { get; set; } = new List<WarriorWeapon>();
    }
}
