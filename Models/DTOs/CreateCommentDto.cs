using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class CreateCommentDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; } // links comment to post it is made on
    }
}
