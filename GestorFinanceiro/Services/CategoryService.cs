using GestorFinanceiro.Adapter;
using GestorFinanceiro.Dtos;
using GestorFinanceiro.Models;
using GestorFinanceiro.Repositorys;

namespace GestorFinanceiro.Services
{
    public class CategoryService
    {

        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryAdapter _adapter = new();

        public CategoryService(CategoryRepository categoryRepository)
        { 
            _categoryRepository = categoryRepository; 
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(int userId)
        {
            var model = await _categoryRepository.GetAll(userId);
            return model == null ? null : _adapter.Map(model);
        }

        public async Task<IEnumerable<CategoryDto>> GetSystemCategories()
        {
            var models = await _categoryRepository.GetSystemCategories();
            return models == null ? null : _adapter.Map(models);
        }
        // verificar se é a melhor maneira de passar esses dados
        public async Task<IEnumerable<CategoryDto>> GetSystemCategoriesByType(string type)
        {
            var models = await _categoryRepository.GetSystemCategoriesByType(type);
            return models == null ? null : _adapter.Map(models);
        }

        public async Task<IEnumerable<CategoryDto>> GetUserCategories(int userId)
        {
            var models = await _categoryRepository.GetUserCategories(userId);
            return models == null ? null : _adapter.Map(models);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var model = await _categoryRepository.GetById(id);
            return model == null ? null : _adapter.Map(model);
        }

        public async Task<CategoryDto> Create(CategoryDto category)
        {
            category.CreatedAt = DateTime.UtcNow;
            var model = _adapter.Map(category);
            var createdCategory = await _categoryRepository.Create(model);
            return createdCategory == null ? null : _adapter.Map(createdCategory);
        }

        public async Task<CategoryDto> Update(CategoryDto category)
        {
            var model = _adapter.Map(category);
            var UpdatedModel = await _categoryRepository.Update(model);
            return UpdatedModel == null ? null : _adapter.Map(UpdatedModel);
        }

        public async Task<bool> Delete(int id, int userId)
        {
            // Verificar se a categoria existe e é personalizada do usuário
            var existingCategory = await GetById(id);
            if (existingCategory == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            if (existingCategory.IsSystem)
            {
                throw new Exception("Não é possível deletar categorias do sistema");
            }

            if (existingCategory.UserId != userId)
            {
                throw new Exception("Não é possível deletar categorias de outros usuários");
            }

            return await _categoryRepository.Delete(id, userId);
        }




    }
}
