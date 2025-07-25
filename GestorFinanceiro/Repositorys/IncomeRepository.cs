using Dapper;
using Npgsql;
using GestorFinanceiro.Models;
using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Repositorys
{
    public class IncomeRepository
    {
        private readonly string _connectionString;
        public IncomeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IncomeModel> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<IncomeModel>("SELECT * FROM Income WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<IncomeModel>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<IncomeModel>("SELECT * FROM Income");
        }

        public async Task<IncomeModel> Create(IncomeModel income)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO Income(name, amount, date, description, category)
                        VALUES (@Name, @Amount, @Date, @Description, @Category)
                        RETURNING id";
            income.Id = await connection.ExecuteScalarAsync<int>(sql, income);
            return income;
        }

        public async Task<IncomeModel> Update(IncomeModel income) 
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var existingIncome = await GetById(income.Id);
            var sql = @"UPDATE Income SET name = @Name, amount = @Amount, date = @Date, description = @Description, category = @Category
                        WHERE id = @Id";

            return await connection.ExecuteAsync(sql, income) > 0 ? income : null;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM Income WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

    }
}