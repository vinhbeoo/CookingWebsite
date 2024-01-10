using Microsoft.Identity.Client;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class UserActivityRepository : IUserActivityRepository
    {
        public List<UserActivity> FindUserActivitiesByUserId(int userId) => UserActivityDAO.Instance.FindUserActivitiesByUserId(userId);
        public void LogActivity(UserActivity userActivity) => UserActivityDAO.Instance.LogUserActivity(userActivity);
        public List<UserActivity> GetUserActivities() => UserActivityDAO.Instance.GetUserActivities();
        public List<UserActivity> GetUserActivitiesById(int id) => UserActivityDAO.Instance.GetUserActivitiesById(id);
        public List<UserActivity> DeleteUserActivitiesByUserId(int userId) => UserActivityDAO.Instance.DeleteUserActivitiesByUserId(userId);
    }
}
