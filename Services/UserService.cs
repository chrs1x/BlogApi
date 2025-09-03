using BlogApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlogApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;
        public IEnumerable<User> GetAllUsers() => _users;

        public User? GetUserById(int id) => _users.FirstOrDefault(t => t.Id == id);

        public User CreateUser(CreateUserDto dto) 
        {
            var user = new User
            {
                Id = _nextId++,
                Name = dto.Name,
                Email = dto.Email
            };
            _users.Add(user);
            return user;
        }

        public User UpdateUser(UpdateUserDto dto, int id)
        { 
            var user = GetUserById(id);
            if (user == null) throw new KeyNotFoundException($"No user found with that id.");
            if(!string.IsNullOrWhiteSpace(dto.Name)) { user.Name = dto.Name; }
            if(!string.IsNullOrWhiteSpace(dto.Email)) { user.Email = dto.Email; }
            return user;
        }

        public User DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user == null) return null;
            _users.Remove(user);
            return user;
        }
    }
}
