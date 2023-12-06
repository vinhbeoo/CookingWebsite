using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class RecipeDAO
    {
        private static RecipeDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RecipeDAO Instance
        {
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
                throw new Exception("Error retrieving recipes list: " + ex.Message);
            }
            return list;
        }

        public Recipe GetRecipeById(int id)
        {
            Recipe recipe = new Recipe();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    recipe = context.Recipes.FirstOrDefault(x => x.RecipeId == id);
                }
                if (recipe == null)
                {
                    throw new Exception("Recipe doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return recipe;
        }

        public void SaveRecipe(Recipe recipe, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingRecipe = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);
                    if (existingRecipe != null)
                    {
                        throw new Exception("Recipe already exists");
                    }

                    context.Recipes.Add(recipe);
                    context.SaveChanges();

                    // Log user activity for adding a recipe
                    context.LogUserActivity(userId, "CreateRecipe", $"Created a new recipe with ID {recipe.RecipeId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRecipe(Recipe recipe, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingRecipe = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);

                    if (existingRecipe != null)
                    {
                        context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "UpdateRecipe", $"Updated recipe with ID {recipe.RecipeId}");
                    }
                    else
                    {
                        throw new Exception("Recipe not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRecipe(Recipe recipe, int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var recipeToDelete = context.Recipes.FirstOrDefault(x => x.RecipeId == recipe.RecipeId);
                    if (recipeToDelete == null)
                    {
                        throw new Exception("Recipe is null");
                    }
                    else
                    {
                        context.Recipes.Remove(recipeToDelete);
                        context.SaveChanges();
                        // Log user activity
                        context.LogUserActivity(userId, "DeleteRecipe", $"Deleted recipe with ID {recipe.RecipeId}");
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
