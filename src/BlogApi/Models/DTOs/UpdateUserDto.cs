namespace BlogApi.Models
{
    public class UpdateUserDto
    {
        public string? Name { get; set; } // letz users update their details but not their id
        public string? Email { get; set; }
    }
}
