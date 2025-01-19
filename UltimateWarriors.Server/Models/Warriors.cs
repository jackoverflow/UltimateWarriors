namespace UltimateWarriors.Server.Models {
    public class Warriors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<WarriorWeapon> WarriorWeapons { get; set; } = new List<WarriorWeapon>();
    }
}
