using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Contest> Contests { get; set; } = new List<Contest>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();

    public virtual UserDetail? UserDetail { get; set; }

    public virtual ICollection<UserRegHistory> UserRegHistories { get; set; } = new List<UserRegHistory>();

    public virtual ICollection<WinnerInfo> WinnerInfos { get; set; } = new List<WinnerInfo>();
}
