using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class IngredientsDetail
{
    public int IngredientId { get; set; }

    public int Stt { get; set; }

    public string Description { get; set; } = null!;

    public virtual IngredientsGroup Ingredient { get; set; } = null!;
}
