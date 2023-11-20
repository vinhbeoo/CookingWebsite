using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ProjectLibrary.DataAccess;

namespace ProjectLibrary.ObjectBussiness
{
    public class ContestDAO
    {
        private static ContestDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ContestDAO Instance
        {
            //Singlestone pattern
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ContestDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Contest> GetContests()
        {
            var list = new List<Contest>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.Contests.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Contest FindContestById(int id)
        {
            Contest c = new Contest();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    c = context.Contests.SingleOrDefault(x => x.ContestId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }
        //
        public void SaveContest(Contest c)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Contests.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void UpdateContest(Contest c)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    context.Entry<Contest>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //
        public void DeleteContest(Contest c)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var c1 = context.Contests.SingleOrDefault(x => x.ContestId == c.ContestId);
                    context.Contests.Remove(c1);
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
