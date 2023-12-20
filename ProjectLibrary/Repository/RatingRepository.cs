using ProjectLibrary.DataAccess;
using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public class RatingRepository : IRatingRepository
    {
        public List<Rating> GetRatings() => RatingDAO.Instance.GetRatings();
        public void SaveRating(Rating r) => RatingDAO.Instance.SaveRating(r);
        public Rating GetRatingById(int id) => RatingDAO.Instance.FindRatingById(id);
        public void DeleteRating(Rating r) => RatingDAO.Instance.DeleteRating(r);
        public void UpdateRating(Rating r) => RatingDAO.Instance.UpdateRating(r);
    }
}
