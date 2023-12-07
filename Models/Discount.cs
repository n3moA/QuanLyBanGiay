using System;
using System.Collections.Generic;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class Discount
{
    public string? MaGiamgia { get; set; }

    public DateTime Ngaygiamgia { get; set; }

    public DateTime Ngayhethan { get; set; }

    public decimal Tilegiamgia { get; set; }

    public string? MaDieukien { get; set; }
    public string? DieuKien { get; set; }

}
