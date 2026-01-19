namespace SocialMediaPlatform.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Collection Navigation: Many-to-Many
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
