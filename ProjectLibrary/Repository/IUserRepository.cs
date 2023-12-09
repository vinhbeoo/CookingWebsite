using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserByEmailOrUserName(string input);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);
        Task<bool> ConfirmEmailAsync(string email, string token);

    }
}
