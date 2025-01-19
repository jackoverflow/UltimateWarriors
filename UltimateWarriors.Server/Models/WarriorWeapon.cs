namespace UltimateWarriors.Server.Models {
    public class WarriorWeapon
    {
        public int WarriorId { get; set; }
        public Warriors Warrior { get; set; }
        
        public int WeaponId { get; set; }
        public Weapons Weapon { get; set; }
    }
} 