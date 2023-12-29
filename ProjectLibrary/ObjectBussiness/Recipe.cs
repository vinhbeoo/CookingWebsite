using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string RecipeTitle { get; set; } = null!;

    public string ImageTitle { get; set; } = null!;

    public int Creator { get; set; }

    public DateTime CreateDate { get; set; }

    public int TagId { get; set; }

    public string Description { get; set; } = null!;

    public string? VideoUrl { get; set; }

    public string PrepTime { get; set; } = null!;

    public string CookTime { get; set; } = null!;

    public string TotalTime { get; set; } = null!;

    public string Servings { get; set; } = null!;

    public string Calories { get; set; } = null!;

    public string? Fat { get; set; }

    public string? Carbs { get; set; }

    public string? Protein { get; set; }

    public int? CategoryId { get; set; }

    public int? ContestId { get; set; }

    public Boolean ReadFree { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; }  

    public virtual Contest? Contest { get; set; }

    public virtual User? CreatorNavigation { get; set; }

    public virtual ICollection<IngredientsGroup>? IngredientsGroups { get; set; }

	public virtual ICollection<IngredientsDetail>? IngredientsDetails { get; set; }

	public virtual ICollection<Rating>? Ratings { get; set; }

    public virtual ICollection<RecipesStep>? RecipesSteps { get; set; } 

    public virtual Tag Tag { get; set; } = null!;

    public virtual Category? Category { get; set; }
}
