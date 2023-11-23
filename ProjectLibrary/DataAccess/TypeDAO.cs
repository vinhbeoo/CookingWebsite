using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class TypeDAO
    {
        private static TypeDAO instance = null;
        private static readonly object instanceLock = new object();
        public static TypeDAO Instance
        {
            //Singlestone pattern
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TypeDAO();
                    }
                    return instance;
                }
            }
        }



        public List<ObjectBussiness.Type> GetTypes()
        {
            var type = new List<ObjectBussiness.Type>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    type = context.Types.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return type;
        }

        public ObjectBussiness.Type GetTypeById(int id)
        {
            ObjectBussiness.Type t = new ObjectBussiness.Type();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    t = context.Types.SingleOrDefault(x => x.TypeId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return t;
        }
        //
        public void SaveType(ObjectBussiness.Type t)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Types.Add(t);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateType(ObjectBussiness.Type t)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<ObjectBussiness.Type>(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteType(ObjectBussiness.Type t)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var t1 = context.Types.SingleOrDefault(x => x.TypeId == t.TypeId);
                    context.Types.Remove(t1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
