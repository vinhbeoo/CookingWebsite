using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        public List<Recipe> GetRecipes() => RecipeDAO.Instance.GetRecipes();
        public Recipe GetRecipeById(int id) => RecipeDAO.Instance.GetRecipeById(id);
        public void SaveRecipe(Recipe recipe, int userId) => RecipeDAO.Instance.SaveRecipe(recipe,userId);
        public void UpdateRecipe(Recipe recipe, int userId) => RecipeDAO.Instance.UpdateRecipe(recipe, userId);
        public void DeleteRecipe(Recipe recipe, int userId) =>RecipeDAO.Instance.DeleteRecipe(recipe, userId);
    }
}
