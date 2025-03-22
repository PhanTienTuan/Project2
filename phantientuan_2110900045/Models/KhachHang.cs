using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class KhachHang
{
    public string MaKh { get; set; } = null!;

    public string? TenKh { get; set; }

    public string? DiaChi { get; set; }

    public int? DienThoai { get; set; }

    public string? Gmail { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Hdban> Hdbans { get; set; } = new List<Hdban>();

    public virtual NguoiDung? UsernameNavigation { get; set; }
}
