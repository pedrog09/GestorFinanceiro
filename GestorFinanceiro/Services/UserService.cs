using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Services
{
    public class UserService
    {

        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<UserDto> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _userRepository.GetAll();
        }
        public async Task<UserDto?> Create(UserDto user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            user.Id = await _userRepository.Create(user);
            return user;
        }
        public async Task<UserDto> Update(UserDto user)
        {
            return await _userRepository.Update(user);
        }
        public async Task<bool> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
