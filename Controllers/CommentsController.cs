using BlogApi.Models.DTOs;
using BlogApi.Models;
using BlogApi.Services.CommentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComment() => Ok(await _commentService.GetAllComments());

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment?>> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentById(id);
            return comment == null ? NotFound() : Ok(comment);
        }

        [HttpGet("post/{postId}")]
        public async Task<ActionResult<Comment?>> GetCommentsByPost(int userId)
        {
            var comment = await _commentService.GetCommentsByPost(userId);
            return comment == null ? NotFound() : Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentService.CreateComment(dto);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> UpdateComment([FromBody] UpdateCommentDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedComment = await _commentService.UpdateComment(dto, id);
            return updatedComment == null ? NotFound() : Ok(updatedComment);
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _commentService.DeleteComment(id);
            return success == null ? NotFound() : Ok(success);
        }
    }
}
