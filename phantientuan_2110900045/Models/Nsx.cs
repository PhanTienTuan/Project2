using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class Nsx
{
    public string MaNsx { get; set; } = null!;

    public string? TenNsx { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
