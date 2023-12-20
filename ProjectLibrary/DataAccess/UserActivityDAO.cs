using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class UserActivityDAO
    {
        private static UserActivityDAO instance = null;
        private static readonly object instanceLock = new object();

        public static UserActivityDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserActivityDAO();
                    }
                    return instance;
                }
            }
        }
        public List<UserActivity> GetUserActivities()
        {
            var list = new List<UserActivity>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.UserActivities.ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ném ngoại lệ mới với thông điệp lỗi.
                throw new Exception("Error retrieving contest list: " + ex.Message);
            }
            return list;
        }

        public List<UserActivity> GetUserActivitiesById(int userId)
        {
            var list = new List<UserActivity>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Assuming there is a property named UserId in the UserActivity class
                    list = context.UserActivities.Where(ua => ua.UserId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception and throw a new exception with a custom error message
                throw new Exception("Error retrieving user activities: " + ex.Message);
            }
            return list;
        }


        public void LogUserActivity(UserActivity userActivity)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.UserActivities.Add(userActivity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error logging user activity: " + ex.Message);
            }
        }
    }
}
