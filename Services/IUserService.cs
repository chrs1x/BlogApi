using BlogApi.Models;
using System.Runtime.InteropServices;
namespace BlogApi.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();

        User? GetUserById(int id);

        User CreateUser(User user);

        User UpdateUser(User user);

        User DeleteUser(int id);
    }
}
