using System;
using System.Collections.Generic;

namespace ClassLibrary1.BussinessObject;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? UserId { get; set; }

    public int? RecipeId { get; set; }

    public int? Vote { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
