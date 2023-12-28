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

        public List<UserActivity> FindUserActivitiesByUserId(int userId)
        {
            List<UserActivity> userActivities = new List<UserActivity>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    userActivities = context.UserActivities.Where(x => x.UserId == userId).ToList();
                }

                if (userActivities.Count == 0)
                {
                    throw new Exception("User doesn't exists");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return userActivities;
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
