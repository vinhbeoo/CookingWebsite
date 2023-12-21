﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class NotificationDAO
    {
        private static NotificationDAO instance = null;
        private static readonly object instanceLook = new object();
        public static NotificationDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new NotificationDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Notification> GetNotifications()
        {
            var listNotifications = new List<Notification>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    listNotifications = context.Notifications.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNotifications;
        }

        public Notification GetNotificationById(int notificationId)
        {
            Notification notification = new Notification();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    notification = context.Notifications.FirstOrDefault(x => x.NotificationId == notificationId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return notification;
        }
        public void SaveNotification(Notification notification)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Notifications.Add(notification);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateNotification(Notification notification)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<Notification>(notification).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteNotification(Notification notification)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingNotification = context.Notifications.Find(notification.NotificationId);

                    if (existingNotification != null)
                    {
                        context.Notifications.Remove(existingNotification);
                        context.SaveChanges();
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
