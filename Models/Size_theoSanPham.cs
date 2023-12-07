namespace QuanLyBanGiay_ADMIN.Models
{
    public class Size_theoSanPham
    {
        public string MaSanpham { get; set; } 

        public string? Ten_Sanpham { get; set; }
        public string? Link1 { get; set; }

        public string? Link2 { get; set; }

        public string? Link3 { get; set; }
        public string? Ma_Size { get; set; } = null!;
        public int? SoLuong { get; set; }
    }
}
