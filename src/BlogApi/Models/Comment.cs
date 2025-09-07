namespace BlogApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int PostId { get; set; } // a comment has to be linked to a post
        public Post Post { get; set; }
    }
}
