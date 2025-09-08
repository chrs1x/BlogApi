using BlogApi.Models;
using BlogApi.Services;
using BlogApi.Models.DTOs;
using BlogApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services.CommentService
{
    public class CommentService : ICommentService
    {

        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> GetAllComments() => 
            await _context.Comments.ToListAsync();

        public async Task<Comment?> GetCommentById(int id) => 
            await _context.Comments.FindAsync(id);

        public async Task<IEnumerable<Comment>> GetCommentsByPost(int postId) => 
            await _context.Comments.Where(c => c.PostId == postId).ToListAsync();

        public async Task<Comment> CreateComment(CreateCommentDto dto)
        {
            var comment = new Comment
            {
                Text = dto.Text,
                PostId = dto.PostId
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }


        public async Task<Comment> UpdateComment(UpdateCommentDto dto, int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) throw new KeyNotFoundException($"No comment found with that id.");
            if(!string.IsNullOrWhiteSpace(dto.Text)) { comment.Text = dto.Text; }
            await _context.SaveChangesAsync();  
            return comment;
        }

        public async Task<Comment> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return null;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
