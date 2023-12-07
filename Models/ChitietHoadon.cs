using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class ChitietHoadon
{
    public string MaHoadon { get; set; } = null!;

    public string MaSanpham { get; set; }

    public int? Soluong { get; set; }

    public int? size { get; set; }
    public int? Trangthai { get; set; }

	public int? Giaban { get; set; }

	public virtual Sanpham? MaSanphamNavigation { get; set; }
}
