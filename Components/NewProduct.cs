using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.ModelsDAO;

namespace QuanlyBG.Components
{
	public class NewProduct:ViewComponent
	{
		private readonly BanGiayContext _context;

		public NewProduct(BanGiayContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
            SanphamhienthiDAO sanphamhienthi = new SanphamhienthiDAO(_context);
			ViewBag.Sanphamseco = sanphamhienthi.Laysanseco();
            return View(sanphamhienthi.Laysanphammoi());
        }
	}
}
