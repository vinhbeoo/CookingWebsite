namespace ProjectWebAPI.Application
{
    public class CommentDTO
    {
        public int CommentId { get; set; }

        public int? UserId { get; set; }

        public int? RecipeId { get; set; }

        public string CommentText { get; set; } = null!;

    }
}
