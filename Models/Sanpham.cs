using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class Sanpham
{
    public string? Ma_Sanpham { get; set; }

    public string? Ten_Sanpham { get; set; }

    public string? Mausac { get; set; }

    public int? Giagoc { get; set; }

    public string? MaNhasanxuat { get; set; }

    public string? MaLoai { get; set; }

    public string? MaGiamgia { get; set; } 

    public virtual ICollection<ChitietHoadon> ChitietHoadons { get; set; } = new List<ChitietHoadon>();

    public virtual ICollection<DanhgiaSp> DanhgiaSps { get; set; } = new List<DanhgiaSp>();

    public virtual ICollection<Giohang> Giohangs { get; set; } = new List<Giohang>();
    public virtual ICollection<SanPham_Size> SanPham_Sizes { get; set; } = new List<SanPham_Size>();

    public virtual AnhSp MaSanphamNavigation { get; set; } = null!;
}
