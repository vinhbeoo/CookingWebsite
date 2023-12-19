using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.ObjectBussiness;

public partial class IngredientsGroup
{
    public int IngredientId { get; set; }

    public string? NameIngredients { get; set; }

    public int? RecipeId { get; set; }

    public virtual ICollection<IngredientsDetail>? IngredientsDetails { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
