using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;

namespace QuanLyBanGiay_ADMIN.Controllers
{
    public class LoginController : Controller
    {
        private readonly BanGiayContext context;

        public LoginController(BanGiayContext context)
        {
            this.context = context;
        }
        
		[HttpGet]
		public IActionResult Index(string Username, string Password)
		{
			List<Nguoidung> data = context.Nguoidungs.ToList();
			if (Username is null || Password is null)
			{
				return View();
			}
			foreach (var tk in data)
			{
				if (Username == tk.Username && Password == tk.Password)
				{
					if (tk.MaChucvu == 1)
					{
						HttpContext.Session.SetString("tk", tk.Username);
						HttpContext.Session.SetString("Username", Username);
						HttpContext.Session.SetString("Password", Password);
						return RedirectToAction("Index", "Admin");
					}
					else if (tk.MaChucvu == 2)
					{
						HttpContext.Session.SetString("tk", tk.Username);
						HttpContext.Session.SetString("Username", Username);
						HttpContext.Session.SetString("Password", Password);
						string url = HttpContext.Session.GetString("preUrl");
						return Redirect(url);
					}
				}
			}
			return View();
		}
		public JsonResult SetURL(string url)
		{
			HttpContext.Session.SetString("preUrl", url);
			return Json(new { success = true });
		}

		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SignUp(Nguoidung nguoidung)
		{
			if (nguoidung is not null)
			{
				context.Nguoidungs.Add(nguoidung);
				context.SaveChanges();
				return RedirectToAction("Index", "Login");
			}
			return View(nguoidung);
		}
		public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

    }
}
