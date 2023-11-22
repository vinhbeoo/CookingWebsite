using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IUserDetailRepository
    {
        void SaveUserDetail(UserDetail u);
        UserDetail GetUserDetailById(int id);
        void DeleteUserDetail(UserDetail u);
        void UpdateUserDetail(UserDetail u);
        List<UserDetail> GetUserDetails();
    }
}
