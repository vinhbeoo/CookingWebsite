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
        public void DeleteUser(User user) => UserDAO.Instance.DeleteUser(user);
        public void SaveUser(User user) => UserDAO.Instance.SaveUser(user);
        public void UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
        public List<User> GetUsers() => UserDAO.Instance.GetUsers();
        public User GetUserById(int id) => UserDAO.Instance.FindUserById(id);
        public string CheckAddUser(User user) => UserDAO.Instance.CheckAddUser(user);
        //
        public Task<User> GetUserByEmailOrUserName(string input) => UserDAO.Instance.GetUserByEmailOrUserName(input);
        public Task<bool> ConfirmEmailAsync(string email, string token) => UserDAO.Instance.ConfirmEmailAsync(email, token);
    }
}
