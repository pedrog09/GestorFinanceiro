using GestorFinanceiro.Services;
using GestorFinanceiro.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GestorFinanceiro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenses = await _expenseService.GetAll();
            return Ok(expenses);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var expense = await _expenseService.GetById(id);
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseDto expense)
        {
            ExpenseDto createdExpense = await _expenseService.Create(expense);
            return Ok(createdExpense);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] ExpenseDto expense)
        {
            ExpenseDto updatedExpense = await _expenseService.Update(expense);
            return Ok(updatedExpense);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool deletedExpense = await _expenseService.Delete(id);
            return Ok(deletedExpense);
        }
    }
}
