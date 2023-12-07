using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class Hoadon
{
    public string MaHoadon { get; set; } = null!;

    public int? MaKhachhang { get; set; }

    public DateTime? Ngaydathang { get; set; }

    public string? PhuongthucTt { get; set; }
    public int? TrangthaiHD { get; set; }
    public string? TrangthaiTT { get; set; }
    public string? Diachigiaohang { get; set; }

    //public virtual ChitietHoadon? MaHoadonNavigation { get; set; } = null!;

    //public virtual Nguoidung? MaKhachhangNavigation { get; set; }
}
