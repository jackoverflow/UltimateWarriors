using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using UltimateWarriors.Server.Models;

namespace UltimateWarriors.Server.Repositories
{
    public interface IUltimateWarriorsRepository
    {
        Task<IEnumerable<Warriors>> GetAllWarriorsAsync();
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
    }
}
