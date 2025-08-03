using GestorFinanceiro.Services;
using Microsoft.AspNetCore.Mvc;
using GestorFinanceiro.Dtos;

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

        [HttpGet("system")]
        public async Task<IActionResult> GetSystemCategories()
        {
            var categories = await _categoryService.GetSystemCategories();
            return Ok(categories);
        }
        [HttpGet("system/type/{type}")] 
        public async Task<IActionResult> GetSystemCategoriesByType(string type)
        {
            var categories = await _categoryService.GetSystemCategoriesByType(type);
            return Ok(categories);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCategories(int userId)
        {
            var categories = await _categoryService.GetUserCategories(userId);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto category)
        {
            var createdCategory = await _categoryService.Create(category);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }
    }
}
