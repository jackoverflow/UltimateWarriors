using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateWarriors.Server.Models;

namespace UltimateWarriors.Server.Repositories
{
    public interface IUltimateWarriorsRepository
    {
        Task<IEnumerable<Warrior>> GetAllWarriors();
        Task<IEnumerable<Weapon>> GetAllWeapons();
        Task<Warrior> CreateWarrior(Warrior warrior);
        Task<Weapon> CreateWeapon(Weapon weapon);
        Task<Warrior> GetWarriorById(int id);
        Task DeleteWarrior(int id);
        Task<WarriorWeapon> AssociateWarriorWithWeapon(WarriorWeapon warriorWeapon);
        Task<Warrior> CreateWarriorWithWeapons(WarriorWithWeaponsDto warriorWithWeapons);
    }
} 