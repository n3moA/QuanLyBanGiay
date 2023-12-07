namespace QuanLyBanGiay_ADMIN.Models
{
    public class HienThiYeuCau
    {
        public int ID { get; set; }
        public int? Ma_Nguoidung { get; set; }
        public string? Ten_Nguoidung { get; set; }
        public string? Ma_Hoadon { get; set; }
        public int? TrangThaiYeuCau { get; set; }
        public string? Thong_Bao { get; set; }
        public string? LyDo { get; set; }
        public int? TrangThaiXuLy { get; set; }
        public DateTime? ThoiGian { get; set; }
    }
}
