using System;
using System.Collections.Generic;

namespace ProjectLibrary.ObjectBussiness;

public partial class UserActivity
{
    public int ActivityId { get; set; }

    public int UserId { get; set; }

    public string? Action { get; set; } // Thêm cột Action để lưu hành động (Thêm/Xóa/Sửa).

    public string? Details { get; set; } // Thêm cột Details để lưu thông tin thêm (có thể là JSON).

    public DateTime LogDate { get; set; } = DateTime.Now; // Thêm cột LogDate để lưu ngày ghi log.

    public virtual User User { get; set; } = null!;
}
