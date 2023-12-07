namespace QuanLyBanGiay_ADMIN.Models_User.ViewModels
{
	public class ProductListViewModel
	{
		public IEnumerable<SanphamHienthi> SanphamHienthis { get; set; } = Enumerable.Empty<SanphamHienthi>();

		public PagingInfo PagingInfo { get; set; } = new PagingInfo();
	}
}
