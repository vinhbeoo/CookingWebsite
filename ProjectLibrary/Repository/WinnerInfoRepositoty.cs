using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class WinnerInfoRepositoty : IWinnerInfoRepositoty
    {
        public List<WinnerInfo> GetWinnerInfos() => WinnerInfoDAO.Instance.GetWinnerInfos();
        public WinnerInfo GetWinnerInfoById(int id) => WinnerInfoDAO.Instance.GetWinnerInfoById(id);
        public WinnerInfo GetWinnerInfoByContestId(int contestId) => WinnerInfoDAO.Instance.GetWinnerInfoByContestId(contestId);
        public void SaveWinnerInfo(WinnerInfo winner) => WinnerInfoDAO.Instance.SaveWinnerInfo(winner);
        public void UpdateWinnerInfo(WinnerInfo winner) => WinnerInfoDAO.Instance.UpdateWinnerInfo(winner);
        public void DeleteWinnerInfo(WinnerInfo winner)=> WinnerInfoDAO.Instance.DeleteWinnerInfo(winner);
    }
}
