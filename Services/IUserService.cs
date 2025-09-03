using BlogApi.Models;
using System.Runtime.InteropServices;
namespace BlogApi.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User? GetUserById(int id);

        User CreateUser(CreateUserDto dto);

        User UpdateUser(UpdateUserDto dto, int id);

        User DeleteUser(int id);
    }
}
