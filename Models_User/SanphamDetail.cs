using QuanLyBanGiay_ADMIN.Models;

namespace QuanLyBanGiay_ADMIN.Models_User
{
	public class SanphamDetail
	{
		public string? Masanpham { get; set; }
		public string? TenSanpham { get; set; }
		public int? Giagoc { get; set; }
		public string? link1 { get; set; }
		public string? link2 { get; set; }
		public string? link3 { get; set; }
        public string? Hangsanxuat { get; set; }
        public List<SanPham_Size> sanPhamSizes { get; set; }
		public int? starmark { get; set; }
	}
}
