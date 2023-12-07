using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Infrastructure;
using QuanLyBanGiay_ADMIN.Models_User;
using QuanLyBanGiay_ADMIN.Data;

namespace QuanLyBanGiay_ADMIN.Components 
{
    public class Bill: ViewComponent
    {
        private readonly BanGiayContext _context;
        public Cart? Cart { get; set; }

        public Bill(BanGiayContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {

            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            HttpContext.Session.SetJson("cart", Cart);

            return View(Cart);
        }
    }
}
