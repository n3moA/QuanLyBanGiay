using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class Giohang
{
    public string MaGiohang { get; set; } = null!;

    public int? MaKhachhang { get; set; }

    public string? MaSanpham { get; set; }

    public int? Soluong { get; set; }

    public int? Giaban { get; set; }

    public virtual Nguoidung? MaKhachhangNavigation { get; set; }

    public virtual Sanpham? MaSanphamNavigation { get; set; }
}
