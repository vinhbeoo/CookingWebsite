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

    public virtual IngredientsGroup? Ingredient { get; set; }
}
