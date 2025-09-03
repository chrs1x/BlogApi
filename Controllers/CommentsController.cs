using BlogApi.Models.DTOs;
using BlogApi.Models;
using BlogApi.Services.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("blogapi/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Gets all comments from the database.
        /// </summary>
        /// <returns>A list of Comment objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComment() => Ok(await _commentService.GetAllComments());

        /// <summary>
        /// Finds the comment with the id specified.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        /// <returns>The comment with that id, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment?>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);
            return comment == null ? NotFound() : Ok(comment);
        }

        /// <summary>
        /// Gets all comments for a specific post.
        /// </summary>
        /// <param name="postId">The id of the post.</param>
        /// <returns>A list of comments for that post, or 404 if none are found.</returns>
        [HttpGet("post/{postId}")]
        public async Task<ActionResult<Comment?>> GetCommentsByPost(int userId)
        {
            var comment = await _commentService.GetCommentsByPost(userId);
            return comment == null ? NotFound() : Ok(comment);
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="dto">The data transfer object containing comment details.</param>
        /// <returns>The created comment.</returns>
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentService.CreateComment(dto);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        /// <summary>
        /// Updates an existing comment with the id specified.
        /// </summary>
        /// <param name="dto">The data transfer object containing updated comment details.</param>
        /// <param name="id">The id of the comment to update.</param>
        /// <returns>The updated comment, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> UpdateComment([FromBody] UpdateCommentDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedComment = await _commentService.UpdateComment(dto, id);
            return updatedComment == null ? NotFound() : Ok(updatedComment);
        }

        /// <summary>
        /// Searches comments by their text content.
        /// </summary>
        /// <param name="text">Optional text filter.</param>
        /// <returns>A list of comments that match the search filter.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Comment>>> Search([FromQuery] string? text)
        {
            var comments = await _commentService.GetAllComments();

            if (!String.IsNullOrWhiteSpace(text))
            {
                comments = comments.Where(t => t.Text.Contains(text, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(comments.ToList());
        }

        /// <summary>
        /// Deletes a comment with the id specified.
        /// </summary>
        /// <param name="id">The id of the comment to delete.</param>
        /// <returns>The deleted comment, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _commentService.DeleteComment(id);
            return success == null ? NotFound() : Ok(success);
        }
    }
}
