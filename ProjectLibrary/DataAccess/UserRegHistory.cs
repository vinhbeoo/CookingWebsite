using System;
using System.Collections.Generic;

namespace ProjectLibrary.DataAccess;

public partial class UserRegHistory
{
    public int RegistrationId { get; set; }

    public int? UserId { get; set; }

    public string? SubscriptionType { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Amount { get; set; }

    public virtual User? User { get; set; }
}
