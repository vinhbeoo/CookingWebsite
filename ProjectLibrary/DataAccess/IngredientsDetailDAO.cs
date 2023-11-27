using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class IngredientsDetailDAO
    {
        private static IngredientsDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        public static IngredientsDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new IngredientsDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<IngredientsDetail> GetIngredientsDetails()
        {
            var list = new List<IngredientsDetail>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.IngredientsDetails.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving ingredients details list: " + ex.Message);
            }
            return list;
        }

        public IngredientsDetail GetIngredientsDetailById(int id)
        {
            IngredientsDetail ingredientsDetail = new IngredientsDetail();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    ingredientsDetail = context.IngredientsDetails.FirstOrDefault(x => x.IngredientId == id);
                }
                if (ingredientsDetail == null)
                {
                    throw new Exception("Ingredients detail doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ingredientsDetail;
        }

        public void SaveIngredientsDetail(IngredientsDetail ingredientsDetail)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingIngredient = context.IngredientsDetails.FirstOrDefault(x => x.IngredientId == ingredientsDetail.IngredientId);
                    if (existingIngredient != null)
                    {
                        throw new Exception("Ingredients detail already exists");
                    }

                    context.IngredientsDetails.Add(ingredientsDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateIngredientsDetail(IngredientsDetail ingredientsDetail)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingIngredient = context.IngredientsDetails.FirstOrDefault(x => x.IngredientId == ingredientsDetail.IngredientId);

                    if (existingIngredient != null)
                    {
                        context.Entry(existingIngredient).CurrentValues.SetValues(ingredientsDetail);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Ingredients detail not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteIngredientsDetail(IngredientsDetail ingredientsDetail)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var ingredientToDelete = context.IngredientsDetails.FirstOrDefault(x => x.IngredientId == ingredientsDetail.IngredientId);
                    if (ingredientToDelete == null)
                    {
                        throw new Exception("Ingredients detail is null");
                    }
                    else
                    {
                        context.IngredientsDetails.Remove(ingredientToDelete);
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
