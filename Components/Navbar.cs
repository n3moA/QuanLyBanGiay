using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Infrastructure;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User;

namespace QuanlyBG.Components
{
    public class Navbar : ViewComponent
    {
        private readonly BanGiayContext _context;
        public Cart? Cart { get; set; }

        public Navbar(BanGiayContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart");
            ViewBag.user = HttpContext.Session.GetString("Username");
            if(Cart is null)
            {
                ViewBag.cartnumber = 0;
            }
            else
            {
                ViewBag.cartnumber = Cart.ComputeTotalProducts();
            }
            return View();
        }
    }
}
