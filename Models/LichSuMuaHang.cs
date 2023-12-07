namespace QuanLyBanGiay_ADMIN.Models
{
    public class LichSuMuaHang
    {
        public int id { get; set; }
        public int Ma_Nguoidung { get; set; }
        public string? MaHoadon { get; set; }
        public DateTime? Ngaydathang { get; set; }
        public string? MaSanpham { get; set; }
        public string? Ten_Sanpham { get; set; }
        public string? Link1 { get; set; }

        public string? Link2 { get; set; }

        public string? Link3 { get; set; }
        public int? size { get; set; }
        public int? Soluong { get; set; }
        public int? Giagoc { get; set; }
        public string? MaGiamgia { get; set; }
        public double? Thanhtien { get; set; }

    }
}
