using ProjectLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class NotificationRepository:INotificationRepository
    {
        public List<Notification> GetNotifications() => NotificationDao.Instance.GetNotifications();
        public Notification GetNotificationById(int id) => NotificationDao.Instance.GetNotificationById(id);
        public void SaveNotification(Notification n) => NotificationDao.Instance.SaveNotification(n);
        public void UpdateNotification(Notification n) => NotificationDao.Instance.UpdateNotification(n);
        public void DeleteNotification(Notification n) => NotificationDao.Instance.DeleteNotification(n);
    }
}
