using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public string? EmailConfirmationToken { get; set; }

    public int? RoleId { get; set; }

    public string? Status { get; set; }

    public int UserType { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; }

    public virtual ICollection<Contest>? Contests { get; set; }

    public virtual ICollection<Notification>? Notifications { get; set; }

    public virtual ICollection<Rating>? Ratings { get; set; } 

    public virtual ICollection<Recipe>? Recipes { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserActivity>? UserActivities { get; set; } 

    public virtual UserDetail? UserDetail { get; set; }

    public virtual ICollection<UserRegHistory>? UserRegHistories { get; set; }

    public virtual ICollection<WinnerInfo>? WinnerInfos { get; set; } 
}
