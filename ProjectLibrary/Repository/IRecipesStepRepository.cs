using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IRecipesStepRepository
    {
        List<RecipesStep> GetRecipesSteps();
		List<RecipesStep> GetRecipesStepListById(int id);
		RecipesStep GetRecipesStepById(int id);
        void SaveRecipesStep(RecipesStep recipesStep);
        void UpdateRecipesStep(RecipesStep recipesStep);
        void DeleteRecipesStep(RecipesStep recipesStep);
    }
}
