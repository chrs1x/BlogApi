using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IBlogService
    {
        IEnumerable<Post> GetAllPosts();
        Post? GetPostById(int id);

        Post? GetPostByUser(int userId);

        Post CreatePost(Post post);

        Post UpdatePost(Post post);

        Post DeletePost(int id);
    }
}
