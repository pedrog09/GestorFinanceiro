using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Services.Interfaces
{
    public interface IIncomeService
    {
        Task<IncomeDto> GetById(int id);
        Task<IEnumerable<IncomeDto>> GetAll();
        Task<IncomeDto> Create(IncomeDto income);
        Task<IncomeDto> Update(IncomeDto income);
        Task<bool> Delete(int id);
    }
}
