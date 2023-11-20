using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class Comment
{
    public int CommentId { get; set; }

    public int UserId { get; set; }

    public int RecipeId { get; set; }

    public string CommentText { get; set; } = null!;

    public virtual Recipe? Recipe { get; set; } = null!;

    public virtual User? User { get; set; } = null!;
}
