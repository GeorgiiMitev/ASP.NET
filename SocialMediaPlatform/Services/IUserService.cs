using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services
{
    public interface IUserService
    {
        public const string name = "Name";

        public abstract List<User> GetUsers();

        public abstract User GetUserById(int id);

        public abstract User Create(User user);

        public abstract User UpdateUsers(int id, User user);

        public abstract bool Delete(int id);
    }
}
