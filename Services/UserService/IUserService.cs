using BlogApi.Models;
using System.Runtime.InteropServices;

namespace BlogApi.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User?> GetUserById(int id);

        Task<User> CreateUser(CreateUserDto dto);

        Task<User> UpdateUser(UpdateUserDto dto, int id);

        Task<User> DeleteUser(int id);
    }
}
