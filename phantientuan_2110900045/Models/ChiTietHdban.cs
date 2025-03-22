using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class ChiTietHdban
{
    public string MaHd { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public int? SoLuongBan { get; set; }

    public virtual Hdban MaHdNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
