using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        public void DeleteUserDetail(UserDetail u) => UserDetailDAO.Instance.DeleteUserDetail(u);
        public void SaveUserDetail(UserDetail u) => UserDetailDAO.Instance.SaveUserDetail(u);
        public void UpdateUserDetail(UserDetail u) => UserDetailDAO.Instance.UpdateUserDetail(u);
        public List<UserDetail> GetUserDetails() => UserDetailDAO.Instance.GetUserDetails();
        public UserDetail GetUserDetailById(int id) => UserDetailDAO.Instance.FindUserDetailById(id);
    }
}
