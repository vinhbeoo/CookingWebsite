using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProjectLibrary.ObjectBussiness;

public partial class UserRegHistory
{
    
    public int RegistrationId { get; set; }

    public int? UserId { get; set; }

    public string? SubscriptionType { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    // Thêm trường MemberType để đồng bộ thông tin từ User
    public string? MemberType { get; set; }

    public decimal? Amount { get; set; }

    public virtual User? User { get; set; }
}
