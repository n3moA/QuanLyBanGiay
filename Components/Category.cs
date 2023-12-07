using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;

namespace QuanlyBG.Components
{
	public class Category : ViewComponent
	{
		private readonly BanGiayContext _context;

		public Category(BanGiayContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			ViewBag.Brand = _context.Nhasanxuats.ToList();

			List<Sanpham> Danhsachsp = _context.Sanphams.ToList();

			IEnumerable<string> Color = from sanpham in Danhsachsp select sanpham.Mausac;

			ViewBag.Color = Color.Distinct();

			return View(_context.LoaiGiays.ToList());
		}
	}
}
