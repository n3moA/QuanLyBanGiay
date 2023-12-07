namespace QuanLyBanGiay_ADMIN.Models
{
    public class HienThiHoaDon
    {
        public string MaHoadon { get; set; } = null!;

        public int? MaKhachhang { get; set; }
        public string? TenNguoidung { get; set; }
        public string? sdt { get; set; }
        public string? Diachigiaohang { get; set; }
        public DateTime? Ngaydathang { get; set; }
        public string? PhuongthucTT { get; set; }
        public string? TrangthaiTT { get; set; }
        public double? Thanhtien { get;set; }
        public string? Trangthaihoadon { get; set; }
        public int? TrangthaiHD { get; set; }

    }
}
