using Dapper;
using Npgsql;
using GestorFinanceiro.Models;
using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Repositorys
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserModel> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<UserModel>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<UserModel>("SELECT * FROM Users");
        }

        public async Task<UserModel> Create(UserModel user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO Users (name, email, password, created_at, updated_at)  
                       VALUES (@Name, @Email, @Password, @CreatedAt, @UpdateAt)  
                       RETURNING id";
            user.Id = await connection.ExecuteScalarAsync<int>(sql, user);
            return user;
        }

        public async Task<UserModel> Update(UserModel user)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var existingUser = await GetById(user.Id);
            if (existingUser == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var sql = @"UPDATE Users
                        SET name = @Name, email = @Email create_at = @CreatedAt, update_at = @UpdatedAt 
                        WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, user);

            return affectedRows > 0 ? user : null;

        }

        public async Task<bool> Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var existingUser = await GetById(id);
            if (existingUser == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var sql = "DELETE FROM Users WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}
