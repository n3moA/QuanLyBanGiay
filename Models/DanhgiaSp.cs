using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class DanhgiaSp
{
    public int MaDanhgia { get; set; }

    public string? MaSanpham { get; set; }

    public string? Danhgia { get; set; }

    public int? Sao { get; set; }

    public int? MaNguoidung { get; set; }

    public virtual Nguoidung? MaNguoidungNavigation { get; set; }

    public virtual Sanpham? MaSanphamNavigation { get; set; }
}
