using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<List<User>> GetUsers() => UserDAO.Instance.GetAllUsers();
        public Task<User> GetUserByEmailOrUserName(string input) => UserDAO.Instance.GetUserByEmailOrUserName(input);
        public Task<User> CreateUser(User user) => UserDAO.Instance.CreateUser(user);
        public Task<User> UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
        public Task<User> DeleteUser(User user) => UserDAO.Instance.DeleteUser(user);
        public Task<bool> ConfirmEmailAsync(string email, string token) => UserDAO.Instance.ConfirmEmailAsync(email, token);
    }
}
