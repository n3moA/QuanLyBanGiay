namespace QuanLyBanGiay_ADMIN.Models
{
    public class SanPham_Size
    {
        public string MaSanpham { get; set; } = null!;
        public string Ma_Size { get; set; } = null!;
        public int? SoLuong { get; set; }

        public virtual Sanpham? MaSanPhamNavigation { get; set; } = null!;
        public virtual Size? MaSizeNavigation { get; set; } = null!;
    }
}
