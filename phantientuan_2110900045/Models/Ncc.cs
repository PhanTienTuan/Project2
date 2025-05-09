﻿using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class Ncc
{
    public string MaNcc { get; set; } = null!;

    public string? TenNcc { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
