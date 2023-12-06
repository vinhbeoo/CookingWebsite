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

        public void SaveUserRegHistory(UserRegHistory userRegHistory, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra và xử lý loại thành viên và loại đăng ký
                    HandleMemberAndSubscriptionTypes(userRegHistory);

                    context.UserRegHistories.Add(userRegHistory);
                    context.SaveChanges();

                    // Log user activity for adding a user registration history
                    context.LogUserActivity(userId, "SaveUserRegHistory", $"Saved user registration history with ID {userRegHistory.RegistrationId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUserRegHistory(UserRegHistory userRegHistory, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra và xử lý loại thành viên và loại đăng ký
                    HandleMemberAndSubscriptionTypes(userRegHistory);

                    var existingUserRegHistory = context.UserRegHistories.FirstOrDefault(x => x.RegistrationId == userRegHistory.RegistrationId);

                    if (existingUserRegHistory != null)
                    {
                        context.Entry(existingUserRegHistory).CurrentValues.SetValues(userRegHistory);
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "UpdateUserRegHistory", $"Updated user registration history with ID {userRegHistory.RegistrationId}");
                    }
                    else
                    {
                        throw new Exception("User registration history not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUserRegHistory(UserRegHistory userRegHistory, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var userRegHistoryToDelete = context.UserRegHistories.FirstOrDefault(x => x.RegistrationId == userRegHistory.RegistrationId);
                    if (userRegHistoryToDelete == null)
                    {
                        throw new Exception("User registration history is null");
                    }
                    else
                    {
                        context.UserRegHistories.Remove(userRegHistoryToDelete);
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "DeleteUserRegHistory", $"Deleted user registration history with ID {userRegHistory.RegistrationId}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Phương thức xử lý loại thành viên và loại đăng ký
        private void HandleMemberAndSubscriptionTypes(UserRegHistory userRegHistory)
        {
            // Lấy thông tin thành viên từ bảng User
            //var user = UserDAO.Instance.GetUserById(userRegHistory.UserId ?? 0);
            //if (user != null)
            //{
            //    // Lưu thông tin thành viên vào UserRegHistory
            //    userRegHistory.MemberType = user.MemberType;
            //    userRegHistory.SubscriptionType = user.SubscriptionType;
            //}
        }
    }
}

