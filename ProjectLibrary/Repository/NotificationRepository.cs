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
        public void SaveNotification(Notification notification) => NotificationDAO.Instance.SaveNotification(notification);
        public void UpdateNotification(Notification notification) => NotificationDAO.Instance.UpdateNotification(notification);
        public void DeleteNotification(Notification notification) => NotificationDAO.Instance.DeleteNotification(notification);
    }
}
