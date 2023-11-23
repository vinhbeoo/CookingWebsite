using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class TagRepository : ITagRepository
    {
        public List<Tag> GetTags() => TagDAO.Instance.GetTags();
        public void SaveTag(Tag t) => TagDAO.Instance.SaveTag(t);
        public Tag GetTagById(int id) => TagDAO.Instance.GetTagById(id);
        public void DeleteTag(Tag t) => TagDAO.Instance.DeleteTag(t);
        public void UpdateTag(Tag t) => TagDAO.Instance.UpdateTag(t);
    }
}
