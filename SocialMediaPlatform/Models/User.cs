using System.Reflection.Metadata;

namespace SocialMediaPlatform.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Reference Navigation: One-to-One
        public Blog? Blog { get; set; }
    }
}
