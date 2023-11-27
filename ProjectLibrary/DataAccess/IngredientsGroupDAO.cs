using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.ObjectBussiness;

namespace ProjectLibrary.DataAccess
{
    public class IngredientsGroupDAO
    {
        private static IngredientsGroupDAO instance = null;
        private static readonly object instanceLock = new object();

        public static IngredientsGroupDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new IngredientsGroupDAO();
                    }
                    return instance;
                }
            }
        }

        public List<IngredientsGroup> GetIngredientsGroups()
        {
            var list = new List<IngredientsGroup>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.IngredientsGroups.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving ingredients groups list: " + ex.Message);
            }
            return list;
        }

        public IngredientsGroup GetIngredientsGroupById(int id)
        {
            IngredientsGroup ingredientsGroup = new IngredientsGroup();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    ingredientsGroup = context.IngredientsGroups.FirstOrDefault(x => x.IngredientId == id);
                }
                if (ingredientsGroup == null)
                {
                    throw new Exception("Ingredients group doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ingredientsGroup;
        }

        public void SaveIngredientsGroup(IngredientsGroup ingredientsGroup)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingGroup = context.IngredientsGroups.FirstOrDefault(x => x.IngredientId == ingredientsGroup.IngredientId);
                    if (existingGroup != null)
                    {
                        throw new Exception("Ingredients group already exists");
                    }

                    context.IngredientsGroups.Add(ingredientsGroup);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateIngredientsGroup(IngredientsGroup ingredientsGroup)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingGroup = context.IngredientsGroups.FirstOrDefault(x => x.IngredientId == ingredientsGroup.IngredientId);

                    if (existingGroup != null)
                    {
                        context.Entry(existingGroup).CurrentValues.SetValues(ingredientsGroup);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Ingredients group not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteIngredientsGroup(IngredientsGroup ingredientsGroup)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var groupToDelete = context.IngredientsGroups.FirstOrDefault(x => x.IngredientId == ingredientsGroup.IngredientId);
                    if (groupToDelete == null)
                    {
                        throw new Exception("Ingredients group is null");
                    }
                    else
                    {
                        context.IngredientsGroups.Remove(groupToDelete);
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
