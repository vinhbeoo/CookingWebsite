using ProjectLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.ObjectBussiness
{
    public class RecipesStepDAO
    {

        private static RecipesStepDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RecipesStepDAO Instance
        {
            //Singlestone pattern
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

        public List<RecipesStep> GetRecipesStep()
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
                throw new Exception(ex.Message);
            }
            return list;
        }

        public RecipesStep GetRecipesStepById(int id)
        {
            RecipesStep r = new RecipesStep();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    r = context.RecipesSteps.SingleOrDefault(x => x.RecipeId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        }
        //
        public void SaveRecipesStep(RecipesStep r)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.RecipesSteps.Add(r);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateRecipesStep(RecipesStep r)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<RecipesStep>(r).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteRecipesStep(RecipesStep r)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var r1 = context.RecipesSteps.SingleOrDefault(x => x.RecipeId == r.RecipeId);
                    context.RecipesSteps.Remove(r1);
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
