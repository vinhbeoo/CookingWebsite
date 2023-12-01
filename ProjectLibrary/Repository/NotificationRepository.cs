using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public List<Notification> GetNotifications() => NotificationDAO.Instance.GetNotifications();
        public Notification GetNotificationById(int id) => NotificationDAO.Instance.GetNotificationById(id);
        public void SaveNotification(Notification notification, int userId) => NotificationDAO.Instance.SaveNotification(notification, userId);
        public void UpdateNotification(Notification notification, int userId) => NotificationDAO.Instance.UpdateNotification(notification, userId);
        public void DeleteNotification(Notification notification, int userId) => NotificationDAO.Instance.DeleteNotification(notification, userId);
    }
}
