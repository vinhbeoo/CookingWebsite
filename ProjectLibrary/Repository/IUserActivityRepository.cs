using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IUserActivityRepository
    {
        void LogActivity(UserActivity userActivity);
        List<UserActivity> FindUserActivitiesByUserId(int userId);
        List<UserActivity> GetUserActivities();
    }
}
