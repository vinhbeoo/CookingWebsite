using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? UserId { get; set; }

    public int? RecipeId { get; set; }

    public string CommentText { get; set; } = null!;

	public DateTime CreateDate { get; set; }

	public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
