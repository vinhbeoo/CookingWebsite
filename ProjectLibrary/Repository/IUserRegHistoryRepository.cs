using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IUserRegHistoryRepository
    {
        void SaveUserRegHistory(UserRegHistory userRegHistory, int userId);
        UserRegHistory GetUserRegHistoryById(int id);
        void DeleteUserRegHistory(UserRegHistory userRegHistory, int userId);
        void UpdateUserRegHistory(UserRegHistory userRegHistory, int userId);
    }
}
