using Microsoft.AspNetCore.Http.HttpResults;
using SocialMediaPlatform.Data;
using SocialMediaPlatform.Models;

namespace SocialMediaPlatform.Services
{
    public class UserService : IUserService
    {
        private DbContext _dbContext;
        public UserService(DbContext db)
        {
            _dbContext = db;    
        }
        public User Create(User user)
        {
            _dbContext.Users.Add(user);
            return user;
        }

        public bool Delete(int id)
        {
            User deletedUser = GetUserById(id); 
            return _dbContext.Users.Remove(deletedUser);
        }
        

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users;
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
