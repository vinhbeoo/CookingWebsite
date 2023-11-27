using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class RecipesStepDAO
    {
        private static RecipesStepDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RecipesStepDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RecipesStepDAO();
                    }
                    return instance;
                }
            }
        }

        public List<RecipesStep> GetRecipesSteps()
        {
            var list = new List<RecipesStep>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.RecipesSteps.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving recipes steps list: " + ex.Message);
            }
            return list;
        }

        public RecipesStep GetRecipesStepById(int id)
        {
            RecipesStep recipesStep = new RecipesStep();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    recipesStep = context.RecipesSteps.FirstOrDefault(x => x.RecipeId == id);
                }
                if (recipesStep == null)
                {
                    throw new Exception("Recipes step doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return recipesStep;
        }

        public void SaveRecipesStep(RecipesStep recipesStep)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingStep = context.RecipesSteps.FirstOrDefault(x => x.RecipeId == recipesStep.RecipeId);
                    if (existingStep != null)
                    {
                        throw new Exception("Recipes step already exists");
                    }

                    context.RecipesSteps.Add(recipesStep);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRecipesStep(RecipesStep recipesStep)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingStep = context.RecipesSteps.FirstOrDefault(x => x.RecipeId == recipesStep.RecipeId);

                    if (existingStep != null)
                    {
                        context.Entry(existingStep).CurrentValues.SetValues(recipesStep);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Recipes step not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRecipesStep(RecipesStep recipesStep)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var stepToDelete = context.RecipesSteps.FirstOrDefault(x => x.RecipeId == recipesStep.RecipeId);
                    if (stepToDelete == null)
                    {
                        throw new Exception("Recipes step is null");
                    }
                    else
                    {
                        context.RecipesSteps.Remove(stepToDelete);
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
