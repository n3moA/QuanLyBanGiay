using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanGiay_ADMIN.Controllers
{
	public class PageNotFoundController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
