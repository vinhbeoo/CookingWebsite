using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Tag
{
    public int IdTags { get; set; }

    public string? NameTags { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
