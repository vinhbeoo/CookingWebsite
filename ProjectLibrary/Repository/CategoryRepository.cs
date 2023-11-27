using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.Instance.GetCategories();
        public void SaveCategory(Category category) => CategoryDAO.Instance.SaveCategory(category);
        public Category GetCategoryById(int id) => CategoryDAO.Instance.FindCategoryById(id);
        public void DeleteCategory(Category category) => CategoryDAO.Instance.DeleteCategory(category);
        public void UpdateCategory(Category category) => CategoryDAO.Instance.UpdateCategory(category);
    }
}
