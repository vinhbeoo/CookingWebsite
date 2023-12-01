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
