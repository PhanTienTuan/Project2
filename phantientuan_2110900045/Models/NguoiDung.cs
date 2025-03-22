using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class NguoiDung
{
    public string Usename { get; set; } = null!;

    public string? Password { get; set; }

    public byte? LoaiUser { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
