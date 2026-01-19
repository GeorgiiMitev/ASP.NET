using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Data
{
    public class DbContext
    {
        public List<User> Users { get; set; } = new List<User>()
        {
            new User()
            {
                Id = 1,
                Name = "Ivan",
                Password = "123456",
                
            },
            new User()
            {
                Id = 2,
                Name = "Georgi",
                Password = "123456",
                
            },
            new User()
            {
                Id = 3,
                Name = "Alex",
                Password = "123456",
                
            },
            new User()
            {
                Id = 4,
                Name = "Ivailo",
                Password = "123456",
               
            },
        };
    }
}
