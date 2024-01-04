using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class UserPaymentHistoryDAO
    {
        private static UserPaymentHistoryDAO instance = null;
        private static readonly object instanceLook = new object();
        public static UserPaymentHistoryDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new UserPaymentHistoryDAO();
                    }
                    return instance;
                }
            }
        }
        public List<UserPaymentHistory> GetNotifications()
        {
            var listPaymentHistorys = new List<UserPaymentHistory>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    listPaymentHistorys = context.UserPaymentHistories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPaymentHistorys;
        }

        public UserPaymentHistory GetNotificationById(int userPaymentHistoryId)
        {
            UserPaymentHistory userPaymentHistory = new UserPaymentHistory();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    userPaymentHistory = context.UserPaymentHistories.FirstOrDefault(x => x.PaymentHistoryId == userPaymentHistoryId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return userPaymentHistory;
        }
        public void SaveNotification(UserPaymentHistory userPaymentHistory, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.UserPaymentHistories.Add(userPaymentHistory);
                    context.SaveChanges();
                    // Log user activity for adding a notification
                    context.LogUserActivity(userId, "CreateNotification", $"Created a new notification with ID {userPaymentHistory.PaymentHistoryId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateNotification(UserPaymentHistory userPaymentHistory, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<UserPaymentHistory>(userPaymentHistory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    // Log user activity
                    context.LogUserActivity(userId, "UpdateNotification", $"Updated notification with ID {userPaymentHistory.PaymentHistoryId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteNotification(UserPaymentHistory userPaymentHistory, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingNotification = context.UserPaymentHistories.Find(userPaymentHistory.PaymentHistoryId);

                    if (existingNotification != null)
                    {
                        context.UserPaymentHistories.Remove(existingNotification);
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "DeleteNotifications", $"Deleted Notifications with ID {userPaymentHistory.PaymentHistoryId}");
                    }
                    else
                    {
                        // Thông báo rằng không tồn tại thông báo để xóa
                        throw new InvalidOperationException("Không tồn tại thông báo để xóa");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
