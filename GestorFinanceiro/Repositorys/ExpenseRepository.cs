using Dapper;
using Npgsql;
using GestorFinanceiro.Models;
using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Repositorys
{
    public class ExpenseRepository
    {
        private readonly string _connectionString;
        public ExpenseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ExpenseModel> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<ExpenseModel>("SELECT * FROM Income WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<ExpenseModel>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<ExpenseModel>("SELECT * FROM Income");
        }

        public async Task<ExpenseModel> Create(ExpenseModel expense)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO Income(name, amount, date, description, category)
                        VALUES (@Name, @Amount, @Date, @Description, @Category)
                        RETURNING id";
            expense.Id = await connection.ExecuteScalarAsync<int>(sql, expense);
            return expense;
        }

        public async Task<ExpenseModel> Update(ExpenseModel expense) 
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var existingExpense = await GetById(expense.Id);
            var sql = @"UPDATE Income SET name = @Name, amount = @Amount, date = @Date, description = @Description, category = @Category
                        WHERE id = @Id";
            return await connection.ExecuteAsync(sql, expense) > 0 ? expense : null;
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