namespace QuanLyBanGiay_ADMIN.Models_User
{
	public class Hoadonhienthi
	{
		public string? maHoaDon { get; set; }
		public string? Ngaydathang { get; set; }
		public string? Tongtien { get; set; }
		public string? Phuongthucthanhtoan { get; set; }
		public string? Tennguoinhan { get; set; }
		public string? Sodienthoai { get; set; }
		public string? Diachigiaohang { get; set; }
        public List<CartLine>? Danhsachhang { get; set; }
    }
}
