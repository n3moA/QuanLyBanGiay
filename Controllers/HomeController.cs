using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User;
using QuanLyBanGiay_ADMIN.ModelsDAO;
using System.Diagnostics;

namespace QuanLyBanGiay_ADMIN.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly BanGiayContext _context;

		public HomeController(ILogger<HomeController> logger, BanGiayContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ShopProduct()
		{
			SanphamhienthiDAO sanphamhienthi = new SanphamhienthiDAO(_context);
			return View(sanphamhienthi.GetAll());
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
