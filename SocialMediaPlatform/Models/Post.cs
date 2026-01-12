namespace SocialMediaPlatform.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        //One-to-many
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();






    }
}
