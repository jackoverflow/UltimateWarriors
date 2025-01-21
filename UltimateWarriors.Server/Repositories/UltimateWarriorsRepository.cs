using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using UltimateWarriors.Server.Models;

namespace UltimateWarriors.Server.Repositories
{
    public class UltimateWarriorsRepository : IUltimateWarriorsRepository
    {
        private readonly string _connectionString;

        public UltimateWarriorsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), 
                    "DefaultConnection string is not configured");
        }

        public async Task<IEnumerable<Warrior>> GetAllWarriors()
        {
            const string sql = "SELECT Id, Name, Description FROM public.Warriors;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Warrior>(sql);
        }

        public async Task<IEnumerable<Weapon>> GetAllWeapons()
        {
            const string sql = "SELECT Id, Name, Description FROM public.Weapons;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Weapon>(sql);
        }

        public async Task<Warrior> CreateWarrior(Warrior warrior)
        {
            const string sql = @"
                INSERT INTO public.Warriors (Name, Description)
                VALUES (@Name, @Description)
                RETURNING Id, Name, Description;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Warrior>(sql, warrior);
        }

        public async Task<Weapon> CreateWeapon(Weapon weapon)
        {
            const string sql = @"
                INSERT INTO public.Weapons (Name, Description)
                VALUES (@Name, @Description)
                RETURNING Id, Name, Description;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Weapon>(sql, weapon);
        }

        public async Task<Warrior> GetWarriorById(int id)
        {
            const string sql = "SELECT Id, Name, Description FROM public.Warriors WHERE Id = @Id;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Warrior>(sql, new { Id = id });
        }

        public async Task DeleteWarrior(int id)
        {
            const string sql = "DELETE FROM public.Warriors WHERE Id = @Id;";
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<WarriorWeapon> AssociateWarriorWithWeapon(WarriorWeapon warriorWeapon)
        {
            const string sql = @"
                INSERT INTO public.WarriorWeapon (WarriorId, WeaponId)
                VALUES (@WarriorId, @WeaponId)
                RETURNING WarriorId, WeaponId;";
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QuerySingleAsync<WarriorWeapon>(sql, warriorWeapon);
        }

        public async Task<Warrior> CreateWarriorWithWeapons(Warrior warrior, List<int> weaponIds)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Create the warrior first
                const string createWarriorSql = @"
                    INSERT INTO public.Warriors (Name, Description)
                    VALUES (@Name, @Description)
                    RETURNING Id, Name, Description;";
                
                var createdWarrior = await connection.QuerySingleAsync<Warrior>(createWarriorSql, warrior, transaction);

                // Associate weapons with the warrior
                const string associateWeaponsSql = @"
                    INSERT INTO public.WarriorWeapon (WarriorId, WeaponId)
                    VALUES (@WarriorId, @WeaponId);";

                foreach (var weaponId in weaponIds)
                {
                    await connection.ExecuteAsync(associateWeaponsSql, 
                        new { WarriorId = createdWarrior.Id, WeaponId = weaponId }, 
                        transaction);
                }

                transaction.Commit();
                return createdWarrior;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
