using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class TagDAO
    {
        private static TagDAO instance = null;
        private static readonly object instanceLock = new object();

        public static TagDAO Instance
        {
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
                throw new Exception("Error retrieving tags list: " + ex.Message);
            }
            return list;
        }

        public Tag GetTagById(int id)
        {
            Tag tag = new Tag();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    tag = context.Tags.FirstOrDefault(x => x.TagId == id);
                }
                if (tag == null)
                {
                    throw new Exception("Tag doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tag;
        }

        public void SaveTag(Tag tag)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingTag = context.Tags.FirstOrDefault(x => x.TagId == tag.TagId);
                    if (existingTag != null)
                    {
                        throw new Exception("Tag already exists");
                    }

                    context.Tags.Add(tag);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTag(Tag tag)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingTag = context.Tags.FirstOrDefault(x => x.TagId == tag.TagId);

                    if (existingTag != null)
                    {
                        context.Entry(existingTag).CurrentValues.SetValues(tag);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Tag not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTag(Tag tag)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var tagToDelete = context.Tags.FirstOrDefault(x => x.TagId == tag.TagId);
                    if (tagToDelete == null)
                    {
                        throw new Exception("Tag is null");
                    }
                    else
                    {
                        context.Tags.Remove(tagToDelete);
                        context.SaveChanges();
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
