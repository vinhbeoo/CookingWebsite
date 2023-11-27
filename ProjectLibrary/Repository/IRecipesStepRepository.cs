using System.Collections.Generic;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.Repository
{
    public interface IRecipesStepRepository
    {
        List<RecipesStep> GetRecipesSteps();
        RecipesStep GetRecipesStepById(int id);
        void SaveRecipesStep(RecipesStep recipesStep);
        void UpdateRecipesStep(RecipesStep recipesStep);
        void DeleteRecipesStep(RecipesStep recipesStep);
    }
}
