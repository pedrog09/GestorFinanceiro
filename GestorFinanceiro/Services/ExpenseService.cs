using GestorFinanceiro.Repositorys;
using GestorFinanceiro.Adapter;
using GestorFinanceiro.Dtos;
using GestorFinanceiro.Models;

namespace GestorFinanceiro.Services
{
    public class ExpenseService
    {
        private readonly ExpenseRepository _expenseRepository;
        private readonly ExpenseAdapter _adapter = new();

        public ExpenseService(ExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ExpenseDto> GetById(int id) 
        { 
            var model = await _expenseRepository.GetById(id);
            return model == null ? null : _adapter.Map(model);
        }

        public async Task<IEnumerable<ExpenseDto>> GetAll()
        {
            var models = await _expenseRepository.GetAll();
            return models == null ? null : _adapter.Map(models);
        }

        public async Task<ExpenseDto> Create(ExpenseDto expense)
        {
            expense.Date = DateTime.UtcNow;
            var model = _adapter.Map(expense);
            var createdModel = await _expenseRepository.Create(model);
            return createdModel == null ? null : _adapter.Map(createdModel);
        }



    }
}
