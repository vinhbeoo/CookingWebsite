using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IRecipeStepRepository
    {
        List<RecipesStep> GetRecipesStep();
        void SaveRecipesStep(RecipesStep r);
        RecipesStep GetRecipesStepById(int id);
        void DeleteRecipesStep(RecipesStep r);
        void UpdateRecipesStep(RecipesStep r);
    }
}
