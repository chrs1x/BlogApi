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
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;

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
            await using var context = await GetDbContext();
            var service = new CommentService(context);

            var post1 = new Models.Post { Title = "First Post", Content = "Hello World!" };
            var post2 = new Models.Post { Title = "Second Post", Content = "Hello Again World!" };
            context.Posts.AddRange(post1, post2);
            context.Comments.AddRange(
                new Models.Comment { Text = "Blabla", PostId = post1.Id },
                new Models.Comment { Text = "Hahaha", PostId = post2.Id },
                new Models.Comment { Text = "Wow!", PostId = post2.Id }
                );
            await context.SaveChangesAsync();

            var result = await service.GetAllComments();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetCommentById_ReturnsComment()
        {
            await using var context = await GetDbContext();
            var service = new CommentService(context);

            var post = new Models.Post { Title = "Swim", Content = "Just went for a swim!" };
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var comment = new Models.Comment { Text = "Wow how was it?", PostId = post.Id };
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            var result = await service.GetCommentById(comment.Id);

            Assert.NotNull(result);
            Assert.Equal("Wow how was it?", result.Text);
        }

        [Fact]
        public async Task GetCommentsByPost_ReturnsPostsComments()
        {
            await using var context = await GetDbContext();
            var service = new CommentService(context);

            var post1 = new Models.Post { Title = "First Post", Content = "Hello World!" };
            var post2 = new Models.Post { Title = "Second Post", Content = "Hello Again World!" };
            context.Posts.AddRange(post1, post2);
            context.Comments.AddRange(
                new Models.Comment { Text = "Blabla", PostId = post1.Id },
                new Models.Comment { Text = "Hahaha", PostId = post2.Id },
                new Models.Comment { Text = "Wow!", PostId = post2.Id }
                );
            await context.SaveChangesAsync();

            var result = await service.GetCommentsByPost(post2.Id);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateComment_SavesComment()
        {
            await using var context = await GetDbContext();
            var service = new CommentService(context);

            var post1 = new Models.Post { Title = "First Post", Content = "Hello World!" };
            context.Posts.Add(post1);
            await context.SaveChangesAsync();

            var comment = await service.CreateComment(new Models.CreateCommentDto
            {
                Text = "Hello",
                PostId = post1.Id
            });

            Assert.NotNull(comment);
            Assert.Equal("Hello", comment.Text);
            Assert.Single(context.Comments);
        }

        [Fact]
        public async Task UpdateComment_EditsExistingComment()
        {
            await using var context = await GetDbContext();
            var service = new CommentService(context);

            var post = new Models.Post { Title = "First Post", Content = "Hello World!" };
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var comment = new Models.Comment { Text = "Blabla", PostId = post.Id};
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            var updated = await service.UpdateComment(new Models.UpdateCommentDto
            {
                Text = "New comment"
            }, comment.Id);

            Assert.Equal("New comment", updated.Text);
        }

        [Fact]
        public async Task DeleteComment_RemovesComment()
        {
            await using var context = await GetDbContext();
            var service = new CommentService(context);

            var post = new Models.Post { Title = "Gym", Content = "Just hit legs!" };
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            var comment = new Models.Comment { Text = "Blabla", PostId = post.Id };
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            var deleted = await service.DeleteComment(comment.Id);

            Assert.NotNull(comment);
            Assert.Equal("Blabla", deleted.Text);
            Assert.Empty(context.Comments);
        }
    }
}