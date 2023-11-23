using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess

{
    public class RecipeDAO
    {
        private static RecipeDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RecipeDAO Instance
        {
            //Singlestone pattern
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RecipeDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Recipe> GetRecipes()
        {
            var list = new List<Recipe>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.Recipes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Recipe GetRecipeById(int id)
        {
            Recipe r = new Recipe();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    r = context.Recipes.SingleOrDefault(x => x.RecipeId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        }
        //
        public void SaveRecipe(Recipe r)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Recipes.Add(r);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateRecipe(Recipe r)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<Recipe>(r).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteRecipe(Recipe r)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var r1 = context.Recipes.SingleOrDefault(x => x.RecipeId == r.RecipeId);
                    context.Recipes.Remove(r1);
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
