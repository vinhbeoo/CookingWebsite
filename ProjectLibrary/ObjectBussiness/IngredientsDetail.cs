using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.ObjectBussiness;

public partial class IngredientsDetail
{
    [Key]
    public int IngredientId { get; set; }

    public int Stt { get; set; }

    public string Description { get; set; } = null!;

	public int RecipeId { get; set; }

	public virtual IngredientsGroup? Ingredient { get; set; }

	public virtual Recipe? Recipe { get; set; }
}
