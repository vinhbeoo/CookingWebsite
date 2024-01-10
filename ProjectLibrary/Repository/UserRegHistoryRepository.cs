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
        public List<UserRegHistory> GetUserRegHistoriesByUserId(int userId) => UserRegHistoryDAO.Instance.GetUserRegHistoriesByUserId(userId);
        public UserRegHistory GetUserRegHistoryById(int id) => UserRegHistoryDAO.Instance.FindUserRegHistoryById(id);
        
    }
}
