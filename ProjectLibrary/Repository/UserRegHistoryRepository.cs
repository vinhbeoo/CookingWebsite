using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class UserRegHistoryRepository : IUserRegHistoryRepository
    {
        public List<UserRegHistory> GetUserRegHistories() => UserRegHistoryDAO.Instance.GetUserRegHistories();
        public void SaveUserRegHistory(UserRegHistory userRegHistory, int userId) => UserRegHistoryDAO.Instance.SaveUserRegHistory(userRegHistory, userId);
        public UserRegHistory GetUserRegHistoryById(int id) => UserRegHistoryDAO.Instance.FindUserRegHistoryById(id);
        public void DeleteUserRegHistory(UserRegHistory userRegHistory, int userId) => UserRegHistoryDAO.Instance.DeleteUserRegHistory(userRegHistory, userId);
        public void UpdateUserRegHistory(UserRegHistory userRegHistory, int userId) => UserRegHistoryDAO.Instance.UpdateUserRegHistory(userRegHistory, userId);
    }
}
