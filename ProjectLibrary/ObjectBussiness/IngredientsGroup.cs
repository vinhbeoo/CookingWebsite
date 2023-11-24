using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class IngredientsGroup
{
    public int IngredientId { get; set; }

    public string? NameIngredients { get; set; }

    public int? RecipeId { get; set; }

    public virtual ICollection<IngredientsDetail> IngredientsDetails { get; set; } = new List<IngredientsDetail>();

    public virtual Recipe? Recipe { get; set; }
}
