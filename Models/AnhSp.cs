using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class AnhSp
{
    public string MaSanpham { get; set; } = null!;

    public string? Link1 { get; set; }

    public string? Link2 { get; set; }

    public string? Link3 { get; set; }

    public virtual Sanpham? Sanpham { get; set; }
}
