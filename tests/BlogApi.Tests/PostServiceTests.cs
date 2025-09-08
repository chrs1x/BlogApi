using Xunit;
using Microsoft.EntityFrameworkCore;
using BlogApi.Services.PostService;
using BlogApi.Data;
using System.Linq;
using System.Threading.Tasks;
using BlogApi;
using Microsoft.AspNetCore.SignalR;
using BlogApi.Models;
using BlogApi.Services.UserService;

namespace BlogApi.Tests
{
    public class PostServiceTests
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
        public async Task GetAllPosts_ReturnsAllPosts()
        {
            var context = await GetDbContext();
            var service = new PostService(context);

            context.Posts.Add(new Models.Post { Title = "First Post", Content = "Hello World" });
            context.Posts.Add(new Models.Post { Title = "Gym Session", Content = "Hit legs today!" });
            await context.SaveChangesAsync();

            var posts = await service.GetAllPosts();

            Assert.Equal(2, posts.Count());
        }

        [Fact]
        public async Task GetPostById_ReturnsPost()
        {
            var context = await GetDbContext();
            var service = new PostService(context);

            var post = new Models.Post { Title = "Swim", Content = "Just went for a swim!" };
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var result = await service.GetPostById(post.Id);

            Assert.NotNull(result);
            Assert.Equal("Chris", result.Title);
        }

        [Fact]
        public async Task GetPostsByUser_ReturnsUsersPosts()
        {
            var context = await GetDbContext();
            var service = new PostService(context);

            var user1 = new Models.User { Name = "Enoch", Email = "ebarnes@gmail.com" };
            var user2 = new Models.User { Name = "Barack", Email = "bpeter@gmail.com" };
            context.Users.Add(user1);
            context.Users.Add(user2);
            await context.SaveChangesAsync();

            var post1 = new Models.Post { Title = "Enoch's 1st Post", Content = "Hello!", UserId = user1.Id };
            var post2 = new Models.Post { Title = "Enoch's 2nd Post", Content = "Hello Again!", UserId = user1.Id };
            context.Posts.Add(post1);
            context.Posts.Add(post2);
            await context.SaveChangesAsync();

            var result = await service.GetPostsByUser(user1.Id);

            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.Equal(user1.Id, p.UserId));
        }

        [Fact]
        public async Task CreatePost_SavesPost()
        {
            var context = await GetDbContext();
            var service = new PostService(context);

            var post = await service.CreatePost(new Models.DTOs.CreatePostDto
            {
                Title = "Run",
                Content = "Went for a 5k run!",
                UserId = 1
            });

            Assert.NotNull(post);
            Assert.Equal("Run", post.Title);
            Assert.Single(context.Posts);
        }

        [Fact]
        public async Task UpdatePost_EditsExistingPost()
        {
            var context = await GetDbContext();
            var service = new PostService(context);

            var post = new Models.Post { Title = "Old", Content = "old text" };
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var updated = await service.UpdatePost(new Models.DTOs.UpdatePostDto
            {
                Title = "New",
                Content = "new text"
            }, post.Id);

            Assert.Equal("New", updated.Title);
            Assert.Equal("new text", updated.Content);
        }

        [Fact]
        public async Task DeletePost_RemovesPost()
        {
            var context = await GetDbContext();
            var service = new PostService(context);

            var post = new Models.Post { Title = "Accidental Post", Content = "Didn't mean to post this!" };
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var result = await service.DeletePost(post.Id);

            Assert.NotNull(post);
            Assert.Empty(context.Posts);
        }
    }
}
