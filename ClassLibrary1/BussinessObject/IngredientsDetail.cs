using System;
using System.Collections.Generic;

namespace ClassLibrary1.BussinessObject;

public partial class IngredientsDetail
{
    public int IngredientId { get; set; }

    public int Stt { get; set; }

    public string Description { get; set; } = null!;

    public virtual IngredientsGroup Ingredient { get; set; } = null!;
}
