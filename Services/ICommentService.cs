using BlogApi.Models;
namespace BlogApi.Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments();

        Comment? GetCommentById(int id);

        Comment? GetCommentByPost(int postId);

        Comment CreateComment(Comment comment);

        Comment UpdateComment(Comment comment);

        Comment DeleteComment(int id);
    }
}
