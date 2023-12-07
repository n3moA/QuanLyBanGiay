using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol.Core.Types;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Infrastructure;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User;
using QuanLyBanGiay_ADMIN.ModelsDAO;
using System.Drawing;

namespace QuanLyBanGiay_ADMIN.Controllers
{
	public class CartController : Controller
	{
		public Cart? Cart { get; set; }

		private readonly BanGiayContext _context;

		public CartController(ILogger<HomeController> logger, BanGiayContext context)
		{
			_context = context;
		}

		public IActionResult ViewCart()
		{
			ViewBag.user = HttpContext.Session.GetString("Username");
			Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
			return View(Cart);
		}

		[HttpGet]
		public IActionResult AddToCart(string productID, int quantity, int size = 42)
		{
			ViewBag.user = HttpContext.Session.GetString("Username");
			List<SanphamCart> Danhsachspcart = new SanphamCartDAO(_context).GetAll();
			SanphamCart? product = Danhsachspcart.FirstOrDefault(p => p.Masanpham == productID);
			product.size = size;
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, quantity);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}

		[HttpGet]
		public JsonResult AddtocartModal(string productID, int quantity, int size)
		{
			List<SanphamCart> Danhsachspcart = new SanphamCartDAO(_context).GetAll();
			SanphamCart? product = Danhsachspcart.FirstOrDefault(p => p.Masanpham == productID);
			product.size = size;
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.AddItem(product, quantity);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return Json(new { totalItems = Cart.ComputeTotalProducts() });
		}

		[HttpPost]
		public JsonResult UpdateCart(string productID, int quantity, int size)
		{
			if (quantity == 0)
			{
				RemoveFromCart(productID, size);
			}
			else
			{
				List<SanphamCart> Danhsachspcart = new SanphamCartDAO(_context).GetAll();
				SanphamCart? product = Danhsachspcart.FirstOrDefault(p => p.Masanpham == productID);
				product.size = size;
				if (product != null)
				{
					Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
					Cart.UpdateLine(product, quantity);
					HttpContext.Session.SetJson("cart", Cart);
				}
			}
			return Json(new { totalItems = Cart.ComputeTotalProducts(), totalMoney = Cart.ComputeTotalValue() });
		}

		public IActionResult RemoveFromCart(string productID, int size)
		{
			ViewBag.user = HttpContext.Session.GetString("Username");
			List<SanphamCart> Danhsachspcart = new SanphamCartDAO(_context).GetAll();
			SanphamCart? product = Danhsachspcart.FirstOrDefault(p => p.Masanpham == productID);
			product.size = size;
			if (product != null)
			{
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
				Cart.RemoveLine(product);
				HttpContext.Session.SetJson("cart", Cart);
			}
			return View("Cart", Cart);
		}
	}
}
