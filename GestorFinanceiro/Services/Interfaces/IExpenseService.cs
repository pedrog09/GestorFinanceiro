using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Services.Interfaces
{
    public interface IExpenseService
    {

        Task<ExpenseDto> GetById(int id);
        Task<IEnumerable<ExpenseDto>> GetAll();
        Task<ExpenseDto> Create(ExpenseDto expense);
        Task<ExpenseDto> Update(ExpenseDto expense);
        Task<bool> Delete(int id);
    }
}
