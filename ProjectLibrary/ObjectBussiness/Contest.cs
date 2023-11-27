using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Contest
{
    public int ContestId { get; set; }

    public string ContestName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int? OwnerUserId { get; set; }

    public virtual User? OwnerUser { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<WinnerInfo> WinnerInfos { get; set; } = new List<WinnerInfo>();
}
