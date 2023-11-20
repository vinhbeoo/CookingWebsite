using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class Type
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Recipe>? Recipes { get; set; } = new List<Recipe>();
}
