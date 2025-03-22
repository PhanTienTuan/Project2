using System;
using System.Collections.Generic;

namespace phantientuan_2110900045.Models;

public partial class ChatLieu
{
    public string MaChatLieu { get; set; } = null!;

    public string? ChatLieu1 { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
