using Xunit;
using Microsoft.EntityFrameworkCore;
using BlogApi.Services.CommentService;
using BlogApi.Data;
using System.Linq;
using System.Threading.Tasks;
using BlogApi;
using Microsoft.AspNetCore.SignalR;
using BlogApi.Models;
using BlogApi.Services.PostService;

namespace BlogApi.Tests
{
    public class CommentServiceTests
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
        public async Task GetAllComments_ReturnsAllComments()
        {
            var context = await GetDbContext();
            var service = new CommentService(context);
        }

        [Fact]
        public async Task GetCommentById_ReturnsComment()
        {
            var context = await GetDbContext();
            var service = new CommentService(context);
        }

        [Fact]
        public async Task GetCommentsByPost_ReturnsAllComments()
        {
            var context = await GetDbContext();
            var service = new CommentService(context);
        }

        [Fact]
        public async Task CreateComment_SavesComment()
        {
            var context = await GetDbContext();
            var service = new CommentService(context);
        }

        [Fact]
        public async Task UpdateComment_EditsExistingComment()
        {
            var context = await GetDbContext();
            var service = new CommentService(context);
        }

        [Fact]
        public async Task DeleteComment_RemovesComment()
        {
            var context = await GetDbContext();
            var service = new CommentService(context);
        }
    }
}