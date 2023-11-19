using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class WinnerInfo
{
    public int WinnerId { get; set; }

    public int? ContestId { get; set; }

    public int? WinnerUserId { get; set; }

    public DateTime? WinningDate { get; set; }

    public string? Prize { get; set; }

    public int? Vote { get; set; }

    public virtual Contest? Contest { get; set; }

    public virtual User? WinnerUser { get; set; }
}
