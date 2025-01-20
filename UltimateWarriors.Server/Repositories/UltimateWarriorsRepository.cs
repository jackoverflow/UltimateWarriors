using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
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
    }

    public class UltimateWarriorsRepository : IUltimateWarriorsRepository
    {
        private readonly string _connectionString;

        public UltimateWarriorsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Warrior>> GetAllWarriors()
        {
            const string sql = "SELECT Id, Name, Description FROM Warriors;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Warrior>(sql);
        }

        public async Task<IEnumerable<Weapon>> GetAllWeapons()
        {
            const string sql = "SELECT Id, Name, Description FROM Weapons;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Weapon>(sql);
        }

        public async Task<Warrior> CreateWarrior(Warrior warrior)
        {
            const string sql = @"
                INSERT INTO Warriors (Name, Description)
                VALUES (@Name, @Description)
                RETURNING Id, Name, Description;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Warrior>(sql, warrior);
        }

        public async Task<Weapon> CreateWeapon(Weapon weapon)
        {
            const string sql = @"
                INSERT INTO Weapons (Name, Description)
                VALUES (@Name, @Description)
                RETURNING Id, Name, Description;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Weapon>(sql, weapon);
        }

        public async Task<Warrior> GetWarriorById(int id)
        {
            const string sql = "SELECT Id, Name, Description FROM Warriors WHERE Id = @Id;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Warrior>(sql, new { Id = id });
        }

        public async Task DeleteWarrior(int id)
        {
            const string sql = "DELETE FROM Warriors WHERE Id = @Id;";
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<WarriorWeapon> AssociateWarriorWithWeapon(WarriorWeapon warriorWeapon)
        {
            const string sql = @"
                INSERT INTO WarriorWeapon (WarriorId, WeaponId)
                VALUES (@WarriorId, @WeaponId)
                RETURNING WarriorId, WeaponId;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<WarriorWeapon>(sql, warriorWeapon);
        }
    }
}
