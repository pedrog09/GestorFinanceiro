using GestorFinanceiro.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorFinanceiro.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int userId)
        {
            var categories = await _categoryService.GetAll(userId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            return Ok(category);
        }
    }
}
