namespace QuanLyBanGiay_ADMIN.Models
{
    public class ChiTietHoaDon_ListSP
    {
        public string MaHoadon { get; set; } = null!;

        public string? MaSanpham { get; set; }
        public string? Ten_Sanpham { get; set; }
        public string? Link1 { get; set; }

        public string? Link2 { get; set; }

        public string? Link3 { get; set; }
        public int? Soluong { get; set; }

        public int? size { get; set; }
        public int? Giagoc { get; set; }
        public string? MaGiamgia { get; set; }
        public int? Trangthai { get; set; }
        public Nullable<decimal> Tilegiamgia { get; set; }
    }
}
