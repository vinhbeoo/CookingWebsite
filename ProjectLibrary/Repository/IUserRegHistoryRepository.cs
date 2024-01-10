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
        List<UserRegHistory> GetUserRegHistories();
        List<UserRegHistory> GetUserRegHistoriesByUserId(int userId);
        UserRegHistory GetUserRegHistoryById(int id);
      
    }
}
