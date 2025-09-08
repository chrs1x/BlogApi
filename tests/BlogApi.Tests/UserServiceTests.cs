using Xunit;
using Microsoft.EntityFrameworkCore;
using BlogApi.Services.UserService;
using BlogApi.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Tests
{
    public class UserServiceTests
    {
        private async Task<AppDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            await context.Database.EnsureCreatedAsync();
            return context;
        }


        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            await using var context = await GetDbContext();
            var service = new UserService(context);

            context.Users.Add(new Models.User { Name = "Josh", Email = "jrudge@gmail.com" });
            context.Users.Add(new Models.User { Name = "Stephan", Email = "sstacey@gmail.com" });
            await context.SaveChangesAsync();

            var users = await service.GetAllUsers();

            Assert.Equal(2, users.Count());
        }

        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            await using var context = await GetDbContext();
            var service = new UserService(context);

            var user = new Models.User { Name = "Chris", Email = "conditi@gmail.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var result = await service.GetUserById(user.Id);

            Assert.NotNull(result);
            Assert.Equal("Chris", result.Name);
        }

        [Fact]
        public async Task CreateUser_SavesUser()
        {
            await using var context = await GetDbContext();
            var service = new UserService(context);

            var user = await service.CreateUser(new Models.CreateUserDto
            {
                Name = "Bob",
                Email = "bsmith@gmail.com"
            });

            Assert.NotNull(user);
            Assert.Equal("Bob", user.Name);
            Assert.Single(context.Users);
        }

        [Fact]
        public async Task UpdateUser_ModifiesExistingUser()
        {
            await using var context = await GetDbContext();
            var service = new UserService(context);

            var user = new Models.User { Name = "John", Email = "jevans@gmail.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var updated = await service.UpdateUser(new Models.UpdateUserDto
            {
                Name = "Mark",
                Email = "mpotter@gmail.com"
            }, user.Id);

            Assert.Equal("Mark", updated.Name);
            Assert.Equal("mpotter@gmail.com", updated.Email);
        }

        [Fact]
        public async Task DeleteUser_RemovesUser()
        {
            await using var context = await GetDbContext();
            var service = new UserService(context);

            var user = new Models.User { Name = "Raph", Email = "rcoppins@gmail.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var result = await service.DeleteUser(user.Id);

            Assert.NotNull(user);
            Assert.Empty(context.Users);
        }
    }
}