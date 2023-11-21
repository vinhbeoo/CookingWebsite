using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        public void DeleteUserDetail(UserDetail u) => UserDetailDAO.DeleteUserDetail(u);
        public void SaveUserDetail(UserDetail u) => UserDetailDAO.SaveUserDetail(u);
        public void UpdateUserDetail(UserDetail u) => UserDetailDAO.UpdateUserDetail(u);
        public List<UserDetail> GetUserDetails() => UserDetailDAO.GetUserDetails();
        public UserDetail GetUserDetailById(int id) => UserDetailDAO.FindUserDetailById(id);
    }
}
