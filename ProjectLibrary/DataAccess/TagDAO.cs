using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class TagDAO
    {
        private static TagDAO instance = null;
        private static readonly object instanceLock = new object();

        public static TagDAO Instance
        {
            //Singlestone pattern
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TagDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Tag> GetTags()
        {
            var list = new List<Tag>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.Tags.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Tag GetTagById(int id)
        {
            Tag t = new Tag();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    t = context.Tags.SingleOrDefault(x => x.IdTags == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return t;
        }
        //
        public void SaveTag(Tag t)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Tags.Add(t);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateTag(Tag t)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<Tag>(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteTag(Tag t)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var t1 = context.Tags.SingleOrDefault(x => x.IdTags == t.IdTags);
                    context.Tags.Remove(t1);
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
