﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.ObjectBussiness;

public partial class IngredientsGroup
{
    [Key]
    public int IngredientId { get; set; }

    public string? NameIngredients { get; set; }

    public int? RecipeId { get; set; }

    public string Description { get; set; } = null!;

    public virtual Recipe? Recipe { get; set; }
}
