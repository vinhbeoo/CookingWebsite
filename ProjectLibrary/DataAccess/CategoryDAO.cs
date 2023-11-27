using System;
using System.Collections.Generic;
using System.Linq;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    categories = context.Categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving category list: " + ex.Message);
            }
            return categories;
        }

        public Category FindCategoryById(int categoryId)
        {
            Category category = new Category();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    category = context.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
                }
                if (category == null)
                {
                    throw new Exception("Category not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public void SaveCategory(Category category)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Check for duplicates before adding new
                    var existingCategory = context.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                    if (existingCategory != null)
                    {
                        throw new Exception("Category already exists");
                    }

                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingCategory = context.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);

                    if (existingCategory != null)
                    {
                        context.Entry(existingCategory).CurrentValues.SetValues(category);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Category not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var categoryToDelete = context.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                    if (categoryToDelete == null)
                    {
                        throw new Exception("Category not found");
                    }
                    else
                    {
                        context.Categories.Remove(categoryToDelete);
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
