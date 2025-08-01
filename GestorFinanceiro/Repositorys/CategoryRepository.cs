using Dapper;
using Npgsql;
using GestorFinanceiro.Models;

namespace GestorFinanceiro.Repositorys
{
    public class CategoryRepository
    {
        private readonly string _connectionString;
        
        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<CategoryModel>> GetAll(int userId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
                SELECT * FROM Categories 
                WHERE IsSystem = TRUE OR UserId = @UserId 
                ORDER BY IsSystem DESC, Name";
            return await connection.QueryAsync<CategoryModel>(sql, new { UserId = userId });
        }

        // Buscar apenas categorias padrões
        public async Task<IEnumerable<CategoryModel>> GetSystemCategories()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Categories WHERE IsSystem = TRUE ORDER BY Type, Name";
            return await connection.QueryAsync<CategoryModel>(sql);
        }

        // Buscar categorias padrões por tipo
        public async Task<IEnumerable<CategoryModel>> GetSystemCategoriesByType(string type)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Categories WHERE IsSystem = TRUE AND Type = @Type ORDER BY Name";
            return await connection.QueryAsync<CategoryModel>(sql, new { Type = type });
        }

        // Buscar categorias personalizadas do usuário
        public async Task<IEnumerable<CategoryModel>> GetUserCategories(int userId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Categories WHERE IsSystem = FALSE AND UserId = @UserId ORDER BY Name";
            return await connection.QueryAsync<CategoryModel>(sql, new { UserId = userId });
        }

        public async Task<CategoryModel> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<CategoryModel>(sql, new { Id = id });
        }

        // Criar categoria personalizada
        public async Task<CategoryModel> Create(CategoryModel category)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
                INSERT INTO Categories (Id, Name, Type, UserId, IsSystem, CreatedAt)  
                VALUES (nextval('categories_custom_id_seq'), @Name, @Type, @UserId, FALSE, @CreatedAt)  
                RETURNING Id";
            
            category.Id = await connection.ExecuteScalarAsync<int>(sql, category);
            category.IsSystem = false;
            return category;
        }

        // Atualizar categoria personalizada
        public async Task<CategoryModel> Update(CategoryModel category)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            
            // Verificar se a categoria existe e é personalizada
            var existingCategory = await GetById(category.Id);
            if (existingCategory == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            
            if (existingCategory.IsSystem)
            {
                throw new Exception("Não é possível editar categorias do sistema");
            }

            var sql = @"
                UPDATE Categories
                SET Name = @Name, Type = @Type, UserId = @UserId
                WHERE Id = @Id AND IsSystem = FALSE";

            var affectedRows = await connection.ExecuteAsync(sql, category);
            return affectedRows > 0 ? category : null;
        }

        // Deletar categoria personalizada
        public async Task<bool> Delete(int id, int userId)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var sql = "DELETE FROM Categories WHERE Id = @Id AND IsSystem = FALSE AND UserId = @UserId";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id, UserId = userId });
            return affectedRows > 0;
        }

        // Verificar se uma categoria é do sistema
        public async Task<bool> IsSystemCategory(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT IsSystem FROM Categories WHERE Id = @Id";
            return await connection.ExecuteScalarAsync<bool?>(sql, new { Id = id }) ?? false;
        }
    }
} 