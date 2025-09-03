using BlogApi.Models;
using BlogApi.Models.DTOs;
using BlogApi.Services.PostService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postsService;

        public PostsController(IPostService postService)
        {
            _postsService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts() => Ok(await _postsService.GetAllPosts());

        [HttpGet("{id}")]
        public async Task<ActionResult<Post?>> GetPostById(int id)
        {
            var post = await _postsService.GetPostById(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Post?>> GetPostByUser(int userId)
        {
            var post = await _postsService.GetPostsByUser(userId);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postsService.CreatePost(dto);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost([FromBody] UpdatePostDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedPost = await _postsService.UpdatePost(dto, id);
            return updatedPost == null ? NotFound() : Ok(updatedPost);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Post>>> Search(
            [FromQuery] string? title,
            [FromQuery] string? content)
        {
            var posts = await _postsService.GetAllPosts();

            if (!String.IsNullOrWhiteSpace(title))
            {
                posts = posts.Where(t => t.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (!String.IsNullOrWhiteSpace(content))
            {
                posts = posts.Where(t => t.Content.Contains(content, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(posts.ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _postsService.DeletePost(id);
            return success == null ? NotFound() : Ok(success);
        }
    }
}
