using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class IngredientsGroupDAO
    {
        private static IngredientsGroupDAO instance = null;
        private static readonly object instanceLock = new object();

        public static IngredientsGroupDAO Instance
        {
            //Singlestone pattern
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
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IngredientsGroup GetIngredientsGroupById(int id)
        {
            IngredientsGroup r = new IngredientsGroup();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    r = context.IngredientsGroups.SingleOrDefault(x => x.IngredientId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        }
        //
        public void SaveIngredientsGroup(IngredientsGroup ig)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.IngredientsGroups.Add(ig);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateIngredientsGroup(IngredientsGroup ig)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<IngredientsGroup>(ig).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteIngredientsGroup(IngredientsGroup ig)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var ig1 = context.IngredientsGroups.SingleOrDefault(x => x.IngredientId == ig.IngredientId);
                    context.IngredientsGroups.Remove(ig1);
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
