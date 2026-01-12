namespace SocialMediaPlatform.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //One to many
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        //One to many

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
