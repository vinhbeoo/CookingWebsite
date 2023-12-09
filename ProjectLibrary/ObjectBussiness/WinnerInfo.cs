using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectLibrary.ObjectBussiness;

public partial class WinnerInfo
{
    [Key]
    public int WinnerId { get; set; }

    public int? ContestId { get; set; }

    public int? WinnerUserId { get; set; }

    public DateTime? WinningDate { get; set; }

    public string? Prize { get; set; }

    public int? Vote { get; set; }

    public virtual Contest? Contest { get; set; }

    public virtual User? WinnerUser { get; set; }
}
