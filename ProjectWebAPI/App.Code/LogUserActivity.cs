using ProjectLibrary.ObjectBussiness;

namespace ProjectWebAPI.App.Code
{
    public class LogUserActivity
    {
        public static void LogUserLoginActivity(int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var userActivity = new UserActivity
                    {
                        UserId = userId,
                        Action = "Login",
                        Details = "User logged in",
                        LogDate = DateTime.Now
                    };

                    context.UserActivities.Add(userActivity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi ghi log
                Console.WriteLine("Error logging user login activity: " + ex.Message);
            }
        }

        public static void LogUserRegistrationActivity(int userId)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var userActivity = new UserActivity
                    {
                        UserId = userId,
                        Action = "Register",
                        Details = "User registered",
                        LogDate = DateTime.Now
                    };

                    context.UserActivities.Add(userActivity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi ghi log
                Console.WriteLine("Error logging user registration activity: " + ex.Message);
            }
        }
        public static void LogContestActivity(int userId, int contestId, string action, string details)
        {
            LogActivity(userId, "Contest", contestId, action, details);
        }
        public static void LogCommentActivity(int userId, int commentId, string action, string details)
        {
            LogActivity(userId, "Comment", commentId, action, details);
        }

        public static void LogRecipeActivity(int userId, int recipeId, string action, string details)
        {
            LogActivity(userId, "Recipe", recipeId, action, details);
        }
        private static void LogActivity(int userId, string entityType, int entityId, string action, string details)
        {
            try
            {
                using (var context = new CookingWebsiteContext())
                {
                    var userActivity = new UserActivity
                    {
                        UserId = userId,
                        Action = $"{action} {entityType}",
                        Details = $"{details} {entityType} with ID {entityId}",
                        LogDate = DateTime.Now
                    };

                    context.UserActivities.Add(userActivity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi ghi log
                Console.WriteLine($"Error logging {entityType.ToLower()} {action.ToLower()} activity: {ex.Message}");
            }
        }
    }
}
