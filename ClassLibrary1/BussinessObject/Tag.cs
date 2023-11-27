using System;
using System.Collections.Generic;

namespace ClassLibrary1.BussinessObject;

public partial class Tag
{
    public int TagId { get; set; }

    public string? NameTags { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
