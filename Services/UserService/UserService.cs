using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlogApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context; // replaced in memory list with dbcontext
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsers() =>
            await _context.Users.ToListAsync();

        public async Task<User?> GetUserById(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User> CreateUser(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(UpdateUserDto dto, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException($"No user found with that id.");

            if (!string.IsNullOrWhiteSpace(dto.Name)) { user.Name = dto.Name; }
            if (!string.IsNullOrWhiteSpace(dto.Email)) { user.Email = dto.Email; }

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
