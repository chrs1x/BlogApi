using BlogApi.Models;
namespace BlogApi.Services.CommentService
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComments();

        Task<Comment?> GetCommentById(int id);

        Task<IEnumerable<Comment>>  GetCommentsByPost(int postId);

        Task<Comment> CreateComment(CreateCommentDto dto);

        Task<Comment> UpdateComment(UpdateCommentDto dto, int id);

        Task<Comment> DeleteComment(int id);
    }
}
