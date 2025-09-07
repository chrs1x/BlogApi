using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models.DTOs
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; } 

        [Required]
        public int UserId { get; set; } // links post to user creating it
    }
}
