namespace QuanLyBanGiay_ADMIN.Models
{
    public class ChiTietSanPham
    {
        public string MaSanpham { get; set; } 

        public string? Ten_Sanpham { get; set; }

        public string? Mausac { get; set; }
        public string? MaNhasanxuat { get; set; }
        public string? Ten_Nhasanxuat { get; set; }
        public string? MaLoai { get; set; }
        public string? Ten_loai { get; set; }

        public string? Link1 { get; set; }

        public string? Link2 { get; set; }

        public string? Link3 { get; set; }

        public int? Giagoc { get; set; }
    }
}