using GestorFinanceiro.Dtos;

namespace GestorFinanceiro.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> Create(UserDto user);
        Task<UserDto> Update(UserDto user);
        Task<bool> Delete(int id);
        //task<userdto> authenticate(string email, string password);
        //task<bool> changepassword(int userid, string oldpassword, string newpassword);
        //task<bool> resetpassword(string email);
    }
}
