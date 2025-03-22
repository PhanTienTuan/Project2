using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class Hdban
{
    public string MaHd { get; set; } = null!;

    public DateTime? NgayBan { get; set; }

    public string? MaKh { get; set; }

    public int? TongTien { get; set; }

    public string? DiaChiNhanHang { get; set; }

    public int? SdtnhanHang { get; set; }

    public virtual ICollection<ChiTietHdban> ChiTietHdbans { get; set; } = new List<ChiTietHdban>();

    public virtual KhachHang? MaKhNavigation { get; set; }
}
