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
            var warrior = await connection.QuerySingleOrDefaultAsync<Warrior>(sql, new { Id = id });
            return warrior ?? throw new KeyNotFoundException("Warrior not found.");
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

        public async Task<Warrior> CreateWarriorWithWeapons(WarriorWithWeaponsDto warriorWithWeapons)
        {
            const string sql = @"
                INSERT INTO public.Warriors (Name, Description)
                VALUES (@Name, @Description)
                RETURNING Id;";

            using var connection = new NpgsqlConnection(_connectionString);
            
            // Log the SQL command and parameters from the form
            Console.WriteLine("Executing SQL: " + sql);
            Console.WriteLine("Parameters: Name = {0}, Description = {1}", warriorWithWeapons.Name, warriorWithWeapons.Description);

            var warriorId = await connection.ExecuteScalarAsync<int>(sql, new 
            {
                Name = warriorWithWeapons.Name,
                Description = warriorWithWeapons.Description
            });

            // Associate the warrior with the selected weapons
            foreach (var weaponId in warriorWithWeapons.WeaponIds)
            {
                const string associateSql = @"
                    INSERT INTO public.WarriorWeapon (WarriorId, WeaponId)
                    VALUES (@WarriorId, @WeaponId);";

                // Log the SQL command and parameters for association
                Console.WriteLine("Executing SQL: " + associateSql);
                Console.WriteLine("Parameters: WarriorId = {0}, WeaponId = {1}", warriorId, weaponId);

                await connection.ExecuteAsync(associateSql, new { WarriorId = warriorId, WeaponId = weaponId });
            }

            return new Warrior { Id = warriorId, Name = warriorWithWeapons.Name, Description = warriorWithWeapons.Description };
        }
    }
}
