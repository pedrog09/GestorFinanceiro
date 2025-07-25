using GestorFinanceiro.Services;
using GestorFinanceiro.Dtos;
using Microsoft.AspNetCore.Mvc;



namespace GestorFinanceiro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            UserDto createdUser = await _userService.Create(user);
            return CreatedAtAction(nameof(GetById), new {id = createdUser.Id}, createdUser);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UserDto user) 
        {
            UserDto updatedUser = await _userService.Update(user);
            return Ok(updatedUser);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool deletedUser = await _userService.Delete(id);
            return Ok(deletedUser);
        }
    }
}
