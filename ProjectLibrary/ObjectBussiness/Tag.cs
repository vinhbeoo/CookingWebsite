using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
