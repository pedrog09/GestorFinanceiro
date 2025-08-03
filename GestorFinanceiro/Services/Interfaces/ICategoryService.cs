using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAll(int userId);
        Task<IEnumerable<CategoryDto>> GetSystemCategories();
        Task<IEnumerable<CategoryDto>> GetSystemCategoriesByType(string type);
        Task<IEnumerable<CategoryDto>> GetUserCategories(int userId);
        Task<CategoryDto> GetById(int id);
        Task<CategoryDto> Create(CategoryDto category);
        Task<CategoryDto> Update(CategoryDto category);
        Task<bool> Delete(int id, int UserId);
    }
}
