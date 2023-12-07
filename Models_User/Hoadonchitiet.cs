namespace QuanLyBanGiay_ADMIN.Models_User
{
	public class Hoadonchitiet
	{
		public string? mahoadon { get; set; }
		public int? makhachhang { get; set; }
		public string? diachi { get; set; }
		public string? ngaydathang { get; set; }
		public string? phuongthucthanhtoan { get; set; } = null;
		public string? trangthai { get; set; }
		public List<SanphamHoadon>? danhsachsphd { get; set; }
	}
}


