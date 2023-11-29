using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IUserRepository
    {
        void SaveUser(User user);
        User GetUserById(int id);
        void DeleteUser(User user);
        void UpdateUser(User user);
        List<User> GetUsers();

        //======
        string CheckAddUser(User user);
    }
}
