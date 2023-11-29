using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class WinnerInfoRepository : IWinnerInfoRepository
    {
        public void DeleteWinnerInfo(WinnerInfo winnerInfo) => WinnerInfoDAO.Instance.DeleteWinnerInfo(winnerInfo);
        public void SaveWinnerInfo(WinnerInfo winnerInfo) => WinnerInfoDAO.Instance.SaveWinnerInfo(winnerInfo);
        public void UpdateWinnerInfo(WinnerInfo winnerInfo) => WinnerInfoDAO.Instance.UpdateWinnerInfo(winnerInfo);
        public List<WinnerInfo> GetWinnerInfos() => WinnerInfoDAO.Instance.GetWinnerInfos();
        public WinnerInfo GetWinnerInfoById(int id) => WinnerInfoDAO.Instance.FindWinnerInfoById(id);
    }
}
