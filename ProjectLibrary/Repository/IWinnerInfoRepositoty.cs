using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IWinnerInfoRepositoty
    {
        List<WinnerInfo> GetWinnerInfos();
        WinnerInfo GetWinnerInfoById(int id);
        WinnerInfo GetWinnerInfoByContestId(int contestId);
        void SaveWinnerInfo(WinnerInfo winner);
        void UpdateWinnerInfo(WinnerInfo winner);
        void DeleteWinnerInfo(WinnerInfo winner);
    }
}
