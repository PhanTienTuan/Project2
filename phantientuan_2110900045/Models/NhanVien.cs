using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string? Username { get; set; }

    public string? TenNhanVien { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? SoDienThoai1 { get; set; }

    public string? SoDienThoai2 { get; set; }

    public string? DiaChi { get; set; }

    public string? ChucVu { get; set; }

    public virtual NguoiDung? UsernameNavigation { get; set; }
}
