using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.Repository
{
    public interface IRatingRepository
    {
        List<Rating> GetRatings();
        List<Rating> GetRatingByRecipeId(int recipeId);
        void SaveRating(Rating r);
        Rating GetRatingById(int id);
        Rating GetRatingByUserAndRecipeId(int user, int recipeId);
        List<Rating> GetRatingByContestId(int contestId);
        void DeleteRating(Rating r);
        void UpdateRating(Rating r);
    }
}
