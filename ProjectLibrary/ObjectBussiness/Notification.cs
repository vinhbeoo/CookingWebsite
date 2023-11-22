using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
