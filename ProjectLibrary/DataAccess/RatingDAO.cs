using ProjectLibrary.ObjectBussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary.DataAccess
{
    public class RatingDAO
    {

        private static RatingDAO instance = null;
        private static readonly object instanceLock = new object();

        //Thể hiện duy nhất của lớp ContestDAO, sử dụng mẫu thiết kế Singleton.
        public static RatingDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RatingDAO();
                    }
                    return instance;
                }
            }
        }

        //Lấy danh sách tất cả cuộc thi từ cơ sở dữ liệu.
        public List<Rating> GetRatings()
        {
            var list = new List<Rating>();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    list = context.Ratings.ToList();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ném ngoại lệ mới với thông điệp lỗi.
                throw new Exception("Error retrieving Rating list: " + ex.Message);
            }
            return list;
        }

        public Rating FindRatingById(int ratingId)
        {
            Rating rating = new Rating();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    rating = context.Ratings.FirstOrDefault(x => x.RatingId == ratingId);
                }
                if (rating == null)
                {
                    throw new Exception("Contest doesn't exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rating;
        }

        public Rating GetRatingByUserAndRecipeId(int user, int recipeId)
        {
            Rating rating = new Rating();
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    rating = context.Ratings.FirstOrDefault(x => x.UserId == user && x.RecipeId == recipeId);
                }
                if (rating == null)
                {
                    throw new Exception("Contest doesn't exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rating;
        }
        public void SaveRating(Rating rating)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Kiểm tra trùng lặp trước khi thêm mới
                    var existingRating = context.Ratings.FirstOrDefault(x => x.RatingId == rating.RatingId);
                    if (existingRating != null)
                    {
                        // Xử lý trường hợp trùng lặp (ví dụ: ném ngoại lệ hoặc cập nhật đối tượng đã tồn tại)
                        throw new Exception("Contest already exists");
                    }

                    // Thêm đối tượng vào cơ sở dữ liệu
                    context.Ratings.Add(rating);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRating(Rating rating)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    // Truy vấn đối tượng cần cập nhật từ cơ sở dữ liệu
                    var existingRating = context.Ratings.FirstOrDefault(x => x.RatingId == rating.RatingId);

                    if (existingRating != null)
                    {
                        // Sao chép dữ liệu từ đối tượng đầu vào (c) vào đối tượng đã truy vấn được (existingContest)
                        context.Entry(existingRating).CurrentValues.SetValues(rating);

                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                    }
                    else
                    {
                        // Xử lý trường hợp đối tượng không tồn tại
                        throw new Exception("Rating not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRating(Rating rating)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var ratingDel = context.Ratings.FirstOrDefault(x => x.RatingId == rating.RatingId);
                    if (ratingDel == null)
                    {
                        // Nếu đối tượng không tồn tại, đưa ra thông báo lỗi
                        throw new Exception("Contest is null");
                    }
                    else
                    {
                        context.Ratings.Remove(ratingDel);
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
