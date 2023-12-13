using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public class RecipesStepRepository : IRecipesStepRepository
    {
        public List<RecipesStep> GetRecipesSteps() => RecipesStepDAO.Instance.GetRecipesSteps();
		public List<RecipesStep> GetRecipesStepListById(int recipeId) => RecipesStepDAO.Instance.GetRecipesStepListById(recipeId);
		public RecipesStep GetRecipesStepById(int id) => RecipesStepDAO.Instance.GetRecipesStepById(id);
        public void SaveRecipesStep(RecipesStep recipesStep) => RecipesStepDAO.Instance.SaveRecipesStep(recipesStep);
        public void UpdateRecipesStep(RecipesStep recipesStep) => RecipesStepDAO.Instance.UpdateRecipesStep(recipesStep);
        public void DeleteRecipesStep(RecipesStep recipesStep) => RecipesStepDAO.Instance.DeleteRecipesStep(recipesStep);
    }
}
