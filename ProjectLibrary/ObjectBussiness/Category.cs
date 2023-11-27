using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
