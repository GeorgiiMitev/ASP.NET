using Microsoft.AspNetCore.Http.HttpResults;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services
{
    public class UserService : IUserService
    {
        public User Create(User user)
        {
            return user;
        }

        public bool Delete(int id)
        {
            User deletedUser = GetUserById(id); 
            return DbContext.Users.Remove(deletedUser);
        }
        

        public User GetUserById(int id)
        {
            return DbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public List<User> GetUsers()
        {
            return DbContext.Users;
        }

        public User UpdateUsers(int id, User user)
        {
            User updateUser = GetUserById(id);
            if(updateUser != null)
            {
                updateUser.Name = user.Name;
                updateUser.Email = user.Email;
                updateUser.Phone = user.Phone;
                updateUser.Password = user.Password;

                return updateUser;
            }
            throw new Exception("User was not found");
        }
    }
}
