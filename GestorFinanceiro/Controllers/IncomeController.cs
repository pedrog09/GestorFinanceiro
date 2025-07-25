using GestorFinanceiro.Services;
using GestorFinanceiro.Dtos;
using Microsoft.AspNetCore.Mvc;



namespace GestorFinanceiro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IncomeService _incomeService;

        public IncomeController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var incomes = await _incomeService.GetAll();
            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var income = await _incomeService.GetById(id);
            
            return Ok(income);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IncomeDto income)
        {
            IncomeDto createdIncome = await _incomeService.Create(income);
            return Ok(createdIncome);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] IncomeDto income)
        {
            IncomeDto updatedIncome = await _incomeService.Update(income);
            return Ok(updatedIncome);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool deletedIncome = await _incomeService.Delete(id);
            return Ok(deletedIncome);
        }
    }
}