using GestorFinanceiro.Repositorys;
using GestorFinanceiro.Adapter;
using GestorFinanceiro.Dtos;
using GestorFinanceiro.Models;
using GestorFinanceiro.Services.Interfaces;

namespace GestorFinanceiro.Services
{
    public class UserService : IUserService
    {

        private readonly UserRepository _userRepository;
        private readonly UserAdapter _adapter = new();

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDto> GetById(int id)
        {
            var model = await _userRepository.GetById(id);
            return model == null ? null : _adapter.Map(model);
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var models = await _userRepository.GetAll();
            return models == null ? null : _adapter.Map(models);
        }
        public async Task<UserDto?> Create(UserDto user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            var model = _adapter.Map(user);
            var createdModel = await _userRepository.Create(model);
            return createdModel == null ? null : _adapter.Map(createdModel);
        }
        public async Task<UserDto> Update(UserDto user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            var model = _adapter.Map(user);
            var updatedModel = await _userRepository.Update(model);
            return updatedModel == null ? null: _adapter.Map(updatedModel);
        }
        public async Task<bool> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
