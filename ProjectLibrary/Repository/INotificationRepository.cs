using ProjectLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface INotificationRepository
    {
        List<Notification> GetNotifications();
        Notification GetNotificationById(int id);
        void SaveNotification(Notification n);
        void UpdateNotification(Notification n);
        void DeleteNotification(Notification n);
    }
}
