using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface ITagRepository
    {
        List<Tag> GetTags();
        void SaveTag(Tag t);
        Tag GetTagById(int id);
        void DeleteTag(Tag t);
        void UpdateTag(Tag t);
    }
}
