using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class TagRepository : ITagRepository
    {
        public List<Tag> GetTags() => TagDAO.Instance.GetTags();
        public Tag GetTagById(int id) => TagDAO.Instance.GetTagById(id);
        public void SaveTag(Tag tag) => TagDAO.Instance.SaveTag(tag);
        public void UpdateTag(Tag tag) => TagDAO.Instance.UpdateTag(tag);
        public void DeleteTag(Tag tag) => TagDAO.Instance.DeleteTag(tag);
    }
}
