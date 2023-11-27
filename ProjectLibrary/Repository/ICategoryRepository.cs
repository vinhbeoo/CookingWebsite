using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        void SaveCategory(Category category);
        Category GetCategoryById(int id);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
    }
}