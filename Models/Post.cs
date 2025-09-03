namespace BlogApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int UserId {  get; set; } // a post has to be linked to a user
        public User User { get; set; }

        public List<Comment> Comments { get; set; } = new();
    }
}
