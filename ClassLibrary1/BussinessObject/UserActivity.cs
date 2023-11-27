using System;
using System.Collections.Generic;

namespace ClassLibrary1.BussinessObject;

public partial class UserActivity
{
    public int ActivityId { get; set; }

    public int UserId { get; set; }

    public string? ActivityHistory { get; set; }

    public virtual User User { get; set; } = null!;
}
