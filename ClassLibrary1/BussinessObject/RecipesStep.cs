using System;
using System.Collections.Generic;

namespace ClassLibrary1.BussinessObject;

public partial class RecipesStep
{
    public int Step { get; set; }

    public int? RecipeId { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? VideoUrl { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
