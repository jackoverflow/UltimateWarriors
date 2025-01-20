using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using UltimateWarriors.Server.Models;

namespace UltimateWarriors.Server.Repositories
{
    public interface IUltimateWarriorsRepository
    {
        Task<IEnumerable<Warriors>> GetAllWarriorsAsync();
        Task<int> InsertWarriorAsync(Warriors warrior);
        Task<int> InsertWeaponAsync(Weapons weapon);
        Task InsertWarriorWeaponAsync(WarriorWeapon warriorWeapon);
    }

    public class UltimateWarriorsRepository : IUltimateWarriorsRepository
    {
        private readonly string _connectionString;

        public UltimateWarriorsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Warriors>> GetAllWarriorsAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var warriors = await connection.QueryAsync<Warriors>("SELECT * FROM Warriors");
                return warriors;
            }
        }

        public async Task<int> InsertWarriorAsync(Warriors warrior)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO Warriors (Name, Description) VALUES (@Name, @Description) RETURNING Id;";
                return await connection.ExecuteScalarAsync<int>(sql, warrior);
            }
        }

        public async Task<int> InsertWeaponAsync(Weapons weapon)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO Weapons (Name, Description) VALUES (@Name, @Description) RETURNING Id;";
                return await connection.ExecuteScalarAsync<int>(sql, weapon);
            }
        }

        public async Task InsertWarriorWeaponAsync(WarriorWeapon warriorWeapon)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "INSERT INTO WarriorWeapon (WarriorId, WeaponId) VALUES (@WarriorId, @WeaponId);";
                await connection.ExecuteAsync(sql, warriorWeapon);
            }
        }
    }
}
