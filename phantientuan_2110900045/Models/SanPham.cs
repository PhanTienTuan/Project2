using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class SanPham
{
    public string MaSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public string? MaLoai { get; set; }

    public int? GiaSp { get; set; }

    public int? SoLuongSp { get; set; }

    public string? AnhSp { get; set; }

    public string? MaChatLieu { get; set; }

    public string? MaNcc { get; set; }

    public string? MaNsx { get; set; }

    public virtual ICollection<ChiTietHdban> ChiTietHdbans { get; set; } = new List<ChiTietHdban>();

    public virtual ChatLieu? MaChatLieuNavigation { get; set; }

    public virtual LoaiSp? MaLoaiNavigation { get; set; }

    public virtual Ncc? MaNccNavigation { get; set; }

    public virtual Nsx? MaNsxNavigation { get; set; }
}
