using BlogApi.Models;
using BlogApi.Models.DTOs;
using BlogApi.Services.PostService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlogApi.Controllers
{
    [Route("blogapi/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postsService;

        public PostsController(IPostService postService)
        {
            _postsService = postService;
        }

        /// <summary>
        /// Gets all posts from the database.
        /// </summary>
        /// <returns>A list of Post objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts() => Ok(await _postsService.GetAllPosts());

        /// <summary>
        /// Finds the post with the id specified.
        /// </summary>
        /// <param name="id">The id of the post.</param>
        /// <returns>The post with that id, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Post?>> GetPostById(int id)
        {
            var post = await _postsService.GetPostById(id);
            return post == null ? NotFound() : Ok(post);
        }

        /// <summary>
        /// Gets all posts by a specific user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>A list of posts by that user, or 404 if none are found.</returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Post?>> GetPostByUser(int userId)
        {
            var post = await _postsService.GetPostsByUser(userId);
            return post == null ? NotFound() : Ok(post);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="dto">The data transfer object containing post details.</param>
        /// <returns>The created post.</returns>
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePostDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postsService.CreatePost(dto);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        /// <summary>
        /// Updates an existing post with the id specified.
        /// </summary>
        /// <param name="dto">The data transfer object containing updated post details.</param>
        /// <param name="id">The id of the post to update.</param>
        /// <returns>The updated post, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> UpdatePost([FromBody] UpdatePostDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedPost = await _postsService.UpdatePost(dto, id);
            return updatedPost == null ? NotFound() : Ok(updatedPost);
        }

        /// <summary>
        /// Searches posts by title and/or content.
        /// </summary>
        /// <param name="title">Optional title filter.</param>
        /// <param name="content">Optional content filter.</param>
        /// <returns>A list of posts that match the search filters.</returns>
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

        /// <summary>
        /// Deletes a post with the id specified.
        /// </summary>
        /// <param name="id">The id of the post to delete.</param>
        /// <returns>The deleted post, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _postsService.DeletePost(id);
            return success == null ? NotFound() : Ok(success);
        }
    }
}
