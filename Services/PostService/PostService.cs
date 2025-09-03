using BlogApi.Models;
using BlogApi.Services;
using BlogApi.Models.DTOs;
using BlogApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPosts() =>
            await _context.Posts.ToListAsync();

        public async Task<Post?> GetPostById(int id) => 
            await _context.Posts.FindAsync(id);

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId) => 
            await _context.Posts.Where(p => p.UserId == userId).Include(p => p.Comments).ToListAsync();

        // loads comments along with the posts

        public async Task<Post> CreatePost(CreatePostDto dto)
        {
            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = dto.UserId
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
        public async Task<Post> UpdatePost(UpdatePostDto dto, int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if(post == null) throw new KeyNotFoundException($"No post found with that id.");
            if(!string.IsNullOrWhiteSpace(dto.Title)) { post.Title = dto.Title; }
            if (!string.IsNullOrWhiteSpace(dto.Content)) { post.Content = dto.Content; }
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePost(int id) 
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return null;
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return post;   
        }
    }
}
