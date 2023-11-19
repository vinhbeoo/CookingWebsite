using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class UserDetail
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public virtual User User { get; set; } = null!;
}
