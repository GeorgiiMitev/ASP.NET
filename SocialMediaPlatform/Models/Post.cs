namespace SocialMediaPlatform.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        // Foreign Key for Blog (One-to-Many)
        public int BlogId { get; set; }

        public Blog Blog { get; set; } = null!;

        // Collection Navigation: Many-to-Many
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // New relationship: A Post has many Comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
