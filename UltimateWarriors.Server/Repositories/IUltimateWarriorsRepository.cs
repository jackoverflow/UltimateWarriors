using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateWarriors.Server.Models;
using Npgsql;
using Dapper;

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
        Task<Weapon> GetWeaponById(int id);
    }

    public class UltimateWarriorsRepository : IUltimateWarriorsRepository
    {
        private readonly string _connectionString;

        public UltimateWarriorsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Warrior>> GetAllWarriors()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM public.Warriors";
                return await connection.QueryAsync<Warrior>(sql);
            }
        }

        public async Task<IEnumerable<Weapon>> GetAllWeapons()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM public.Weapons";
                return await connection.QueryAsync<Weapon>(sql);
            }
        }

        public async Task<Warrior> CreateWarrior(Warrior warrior)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO public.Warriors (Name, Description) VALUES (@Name, @Description) RETURNING *";
                return await connection.QuerySingleAsync<Warrior>(sql, warrior);
            }
        }

        public async Task<Weapon> CreateWeapon(Weapon weapon)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO public.Weapons (Name, Description) VALUES (@Name, @Description) RETURNING *";
                return await connection.QuerySingleAsync<Weapon>(sql, weapon);
            }
        }

        public async Task<Warrior> GetWarriorById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM public.Warriors WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<Warrior>(sql, new { Id = id });
            }
        }

        public async Task DeleteWarrior(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "DELETE FROM public.Warriors WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<WarriorWeapon> AssociateWarriorWithWeapon(WarriorWeapon warriorWeapon)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO public.WarriorWeapons (WarriorId, WeaponId) VALUES (@WarriorId, @WeaponId) RETURNING *";
                return await connection.QuerySingleAsync<WarriorWeapon>(sql, warriorWeapon);
            }
        }

        public async Task<Warrior> CreateWarriorWithWeapons(WarriorWithWeaponsDto warriorWithWeapons)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO public.Warriors (Name, Description) VALUES (@Name, @Description) RETURNING *";
                var warrior = await connection.QuerySingleAsync<Warrior>(sql, warriorWithWeapons.Warrior);

                foreach (var weapon in warriorWithWeapons.Weapons)
                {
                    var warriorWeapon = new WarriorWeapon { WarriorId = warrior.Id, WeaponId = weapon.Id };
                    await AssociateWarriorWithWeapon(warriorWeapon);
                }

                return warrior;
            }
        }

        public async Task<Weapon> GetWeaponById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM public.Weapons WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<Weapon>(sql, new { Id = id });
            }
        }
    }
} 