using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class RecipeStepRepository :IRecipeStepRepository
    {
        public List<RecipesStep> GetRecipesStep() => RecipesStepDAO.Instance.GetRecipesStep();
        public void SaveRecipesStep(RecipesStep r) => RecipesStepDAO.Instance.SaveRecipesStep(r);
        public RecipesStep GetRecipesStepById(int id) => RecipesStepDAO.Instance.GetRecipesStepById(id);
        public void DeleteRecipesStep(RecipesStep r) => RecipesStepDAO.Instance.DeleteRecipesStep(r);
        public void UpdateRecipesStep(RecipesStep r) => RecipesStepDAO.Instance.UpdateRecipesStep(r);
    }
}
