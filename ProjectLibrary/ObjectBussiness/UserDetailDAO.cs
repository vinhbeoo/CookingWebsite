using ProjectLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.ObjectBussiness
{
    public class UserDetailDAO
    {
        private static UserDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static UserDetailDAO Instance
        {
            //Singlestone pattern
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public List<UserDetail> GetUserDetails()
        {
            var listUserDetails = new List<UserDetail>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    listUserDetails = context.UserDetails.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listUserDetails;
        }
        public UserDetail FindUserDetailById(int userId)
        {
            UserDetail u = new UserDetail();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    u = context.UserDetails.SingleOrDefault(x => x.UserId == userId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return u;
        }
        public void SaveUserDetail(UserDetail u)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.UserDetails.Add(u);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateUserDetail(UserDetail u)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<UserDetail>(u).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteUserDetail(UserDetail u)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var u1 = context.UserDetails.SingleOrDefault(n => n.UserId == u.UserId);
                    context.UserDetails.Remove(u1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
