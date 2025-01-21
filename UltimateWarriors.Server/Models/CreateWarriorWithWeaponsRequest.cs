namespace UltimateWarriors.Server.Models
{
    public class CreateWarriorWithWeaponsRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> WeaponIds { get; set; }
    }
}