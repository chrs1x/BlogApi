using BlogApi.Models;
using BlogApi.Models.DTOs;

namespace BlogApi.Services.PostService
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post?> GetPostById(int id);

        Task<IEnumerable<Post>> GetPostsByUser(int userId);

        Task<Post> CreatePost(CreatePostDto dto);

        Task<Post> UpdatePost(UpdatePostDto dto, int id);

        Task<Post> DeletePost(int id);
    }
}
