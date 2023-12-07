using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay_ADMIN.Models;

public partial class Nguoidung
{
    public int MaNguoidung { get; set; }
    public string? TenNguoidung { get; set; }
    public string? Gioitinh { get; set; }
    public string? SodienthoaiNv { get; set; }
    public string? DiachiNv { get; set; }
    public int? MaChucvu { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
   
    public virtual ICollection<DanhgiaSp> DanhgiaSps { get; set; } = new List<DanhgiaSp>();

    public virtual ICollection<Giohang> Giohangs { get; set; } = new List<Giohang>();


}
