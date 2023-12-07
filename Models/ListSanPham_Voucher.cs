namespace QuanLyBanGiay_ADMIN.Models
{
    public class ListSanPham_Voucher
    {
        public string Ma_Sanpham { get; set; } = null!;

        public string? Ten_Sanpham { get; set; }

        public string? Mausac { get; set; }

        public int? Giagoc { get; set; }

        public string? Ten_Nhasanxuat { get; set; }

        public string? Ten_loai { get; set; } = null!;

        public string? Link1 { get; set; }

        public string? Link2 { get; set; }

        public string? Link3 { get; set; }
    }
}
