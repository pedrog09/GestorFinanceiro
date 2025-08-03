using GestorFinanceiro.Repositorys;
using GestorFinanceiro.Dtos;
using GestorFinanceiro.Adapter;
using GestorFinanceiro.Services.Interfaces;



namespace GestorFinanceiro.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IncomeRepository _incomeRepository;
        private readonly IncomeAdapter _adapter = new();

        public IncomeService(IncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public async Task<IncomeDto> GetById(int id)
        {
            var model = await _incomeRepository.GetById(id);
            return model == null ? null : _adapter.Map(model);
        }

        public async Task<IEnumerable<IncomeDto>> GetAll()
        {
            var models = await _incomeRepository.GetAll();
            return models == null ? null : _adapter.Map(models);
        }

        public async Task<IncomeDto> Create(IncomeDto income)
        {
            income.Date = DateTime.UtcNow;
            var model = _adapter.Map(income);
            var createdModel = await _incomeRepository.Create(model);
            return createdModel == null ? null : _adapter.Map(createdModel);
        }

        public async Task<IncomeDto> Update(IncomeDto income)
        {
            var model = _adapter.Map(income);
            var updatedModel = await _incomeRepository.Update(model);
            return updatedModel == null ? null : _adapter.Map(updatedModel);
        }
        public async Task<bool> Delete(int id)
        {
            return await _incomeRepository.Delete(id);
        }
    }
}