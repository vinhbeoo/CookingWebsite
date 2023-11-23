using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class IngredientsDetailDAO
    {
        private static IngredientsDetailDAO instance = null;
        private static readonly object instanceLock = new object();

        public static IngredientsDetailDAO Instance
        {
            //Singlestone pattern
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
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IngredientsDetail GetIngredientsDetailById(int id)
        {
            IngredientsDetail r = new IngredientsDetail();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    r = context.IngredientsDetails.SingleOrDefault(x => x.IngredientId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return r;
        }
        //
        public void SaveIngredientsDetail(IngredientsDetail ind)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.IngredientsDetails.Add(ind);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateIngredientsDetail(IngredientsDetail ind)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<IngredientsDetail>(ind).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteIngredientsDetail(IngredientsDetail ind)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var ind1 = context.IngredientsDetails.SingleOrDefault(x => x.IngredientId == ind.IngredientId);
                    context.IngredientsDetails.Remove(ind1);
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
