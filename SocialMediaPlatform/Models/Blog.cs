namespace SocialMediaPlatform.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Foreign Key for User: One-to-One
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Collection Navigation: One-to-Many
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
