using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class WinnerInfoDAO
    {
        private static WinnerInfoDAO instance = null;
        private static readonly object instanceLock = new object();

        public static WinnerInfoDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WinnerInfoDAO();
                    }
                    return instance;
                }
            }
        }

        public List<WinnerInfo> GetWinnerInfos()
        {
            var list = new List<WinnerInfo>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.WinnerInfos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving WinnerInfos list: " + ex.Message);
            }
            return list;
        }

        public WinnerInfo GetWinnerInfoById(int id)
        {
            WinnerInfo winner = new WinnerInfo();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    winner = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == id);
                }
                if (winner == null)
                {
                    throw new Exception("winner doesn't exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return winner;
        }

        public void SaveWinnerInfo(WinnerInfo winner)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingWinnerInfo = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winner.WinnerId);
                    if (existingWinnerInfo != null)
                    {
                        throw new Exception("Tag already exists");
                    }

                    context.WinnerInfos.Add(winner);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateWinnerInfo(WinnerInfo winner)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var existingWinnerInfo = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winner.WinnerId);

                    if (existingWinnerInfo != null)
                    {
                        context.Entry(existingWinnerInfo).CurrentValues.SetValues(winner);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Tag not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteWinnerInfo(WinnerInfo winner)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var WinnerInfoToDelete = context.WinnerInfos.FirstOrDefault(x => x.WinnerId == winner.WinnerId);
                    if (WinnerInfoToDelete == null)
                    {
                        throw new Exception("Tag is null");
                    }
                    else
                    {
                        context.WinnerInfos.Remove(WinnerInfoToDelete);
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
