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
        public void SaveNotification(Notification n) => NotificationDAO.Instance.SaveNotification(n);
        public void UpdateNotification(Notification n) => NotificationDAO.Instance.UpdateNotification(n);
        public void DeleteNotification(Notification n) => NotificationDAO.Instance.DeleteNotification(n);
    }
}
