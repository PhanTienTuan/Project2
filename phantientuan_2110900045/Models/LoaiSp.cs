using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class LoaiSp
{
    public string MaLoai { get; set; } = null!;

    public string? LoaiSp1 { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
