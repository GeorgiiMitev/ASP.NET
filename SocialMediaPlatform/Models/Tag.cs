namespace SocialMediaPlatform.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //many-to-many

        public ICollection<Post> Posts { get; set; } = new List<Post>();

    }
}
