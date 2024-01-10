using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class UserRegHistoryDAO
    {
        private static UserRegHistoryDAO instance = null;
        private static readonly object instanceLock = new object();

        public static UserRegHistoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserRegHistoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<UserRegHistory> GetUserRegHistories()
        {
            var list = new List<UserRegHistory>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.UserRegHistories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user registration histories: " + ex.Message);
            }
            return list;
        }

        public List<UserRegHistory> GetUserRegHistoriesByUserId(int userId)
        {
            var list = new List<UserRegHistory>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.UserRegHistories.Where(x => x.UserId == userId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user registration histories: " + ex.Message);
            }
            return list;
        }

        public UserRegHistory FindUserRegHistoryById(int id)
        {
            UserRegHistory userRegHistory = new UserRegHistory();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    userRegHistory = context.UserRegHistories.FirstOrDefault(x => x.RegistrationId == id);
                }
                if (userRegHistory == null)
                {
                    throw new Exception("User registration history doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userRegHistory;
        }

    }
}

