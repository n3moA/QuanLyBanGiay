using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Infrastructure;
using QuanLyBanGiay_ADMIN.Models_User;
using System.Globalization;
using QuanLyBanGiay_ADMIN.Data;

namespace QuanLyBanGiay_ADMIN.Controllers
{
	public class CheckoutController : Controller
	{
		public Cart? Cart { get; set; }

		private readonly BanGiayContext _context;

		public CheckoutController(BanGiayContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
			HttpContext.Session.SetJson("cart", Cart);
			return View(Cart);

		}

		[HttpGet]
		public IActionResult AddBill(string ngaydathang, string phuongthucthanhtoan, string diachigh, string TenNguoidung, string SodienthoaiNv)
		{
			int mand = 0;
			if (HttpContext.Session.GetString("Username") is not null)
			{
				mand = _context.Nguoidungs.Where(nd => nd.Username == HttpContext.Session.GetString("Username")).FirstOrDefault().MaNguoidung;
			}
			else if (_context.Nguoidungs.Count(n => n.SodienthoaiNv == SodienthoaiNv) == 0)
			{
				Nguoidung newnguoidung = new Nguoidung
				{
					TenNguoidung = TenNguoidung,
					SodienthoaiNv = SodienthoaiNv
				};
				_context.Nguoidungs.Add(newnguoidung);
				_context.SaveChanges();
				mand = _context.Nguoidungs.Where(nd => nd.SodienthoaiNv == SodienthoaiNv).FirstOrDefault().MaNguoidung;
			}
			else
			{
				mand = _context.Nguoidungs.Where(nd => nd.SodienthoaiNv == SodienthoaiNv).FirstOrDefault().MaNguoidung;
			}
			Hoadonhienthi hoadonhienthi = new Hoadonhienthi();
			//Thêm hóa đơn
			string format = "dd/MM/yyyy";
			Hoadon newbill = new Hoadon();
			newbill.Ngaydathang = DateTime.ParseExact(ngaydathang, format, CultureInfo.InvariantCulture);
			newbill.PhuongthucTt = phuongthucthanhtoan;
			newbill.TrangthaiHD = 0;
			newbill.MaKhachhang = mand;
			newbill.MaHoadon = mand + ngaydathang + DateTime.Now.ToString("hh/mm/ss");
			newbill.Diachigiaohang = diachigh;
			newbill.TrangthaiTT = "Chưa thanh toán";
			_context.Hoadons.Add(newbill);
			_context.SaveChanges();

			// Thêm chi tiết hóa đơn
			Cart = HttpContext.Session.GetJson<Cart>("cart");
			foreach (var item in Cart.Lines)
			{
				ChitietHoadon chitiet = new ChitietHoadon();
				chitiet.MaHoadon = newbill.MaHoadon;
				chitiet.MaSanpham = item.Product.Masanpham;
				chitiet.Soluong = item.Quantity;
				chitiet.Giaban = (int)(((decimal)1 - (decimal)item.Product.Tilegiamgia / 100) * (decimal)item.Product.Giagoc);
				chitiet.size = item.Product.size;
				chitiet.Trangthai = 0;
				_context.ChitietHoadons.Add(chitiet);
				_context.SaveChanges();
			}
			// Thêm vào hóa đơn hiển thị
			hoadonhienthi.maHoaDon = newbill.MaHoadon;
			hoadonhienthi.Ngaydathang = ngaydathang;
			hoadonhienthi.Tongtien = Cart.ComputeTotalValue().ToString();
			hoadonhienthi.Phuongthucthanhtoan = newbill.PhuongthucTt;
			hoadonhienthi.Tennguoinhan = TenNguoidung;
			hoadonhienthi.Sodienthoai = SodienthoaiNv;
			hoadonhienthi.Diachigiaohang = newbill.Diachigiaohang;
			hoadonhienthi.Danhsachhang = Cart.Lines;
			// Xóa session cart 
			HttpContext.Session.Remove("cart");
			ViewBag.Username = HttpContext.Session.GetString("Username");
			return View(hoadonhienthi);

		}

		public IActionResult ViewBill(int trangthai = 5)
		{
			if (HttpContext.Session.GetString("Username") is not null)
			{
				// Lấy mã người dùng
				var username = HttpContext.Session.GetString("Username");
				var password = HttpContext.Session.GetString("Password");
				Nguoidung nguoidung = _context.Nguoidungs.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
				int manguoidung = nguoidung.MaNguoidung;
				// Lấy các hóa đơn của người dùng
				List<Hoadonchitiet> ds = new List<Hoadonchitiet>();
				List<Hoadon> danhsachhdnd = new List<Hoadon>();
				if (trangthai > 3)
				{
					danhsachhdnd = _context.Hoadons.Where(hd => hd.MaKhachhang == manguoidung).ToList();
				}
				else
				{
					danhsachhdnd = _context.Hoadons.Where(hd => hd.MaKhachhang == manguoidung && hd.TrangthaiHD == trangthai).ToList();
				}
				foreach (var hd in danhsachhdnd)
				{
					Hoadonchitiet hoadonchitiet = new Hoadonchitiet();
					hoadonchitiet.mahoadon = hd.MaHoadon;
					hoadonchitiet.diachi = hd.Diachigiaohang;
					hoadonchitiet.ngaydathang = hd.Ngaydathang?.ToString("dd-MM-yyyy");
					hoadonchitiet.phuongthucthanhtoan = hd.PhuongthucTt;
					hoadonchitiet.trangthai = exchangeStatus(hd.TrangthaiHD);
					// Thêm sản phẩm vào hóa dơn
					hoadonchitiet.danhsachsphd = _context.SanphamHoadons.FromSqlRaw("GetSanphamhoadon {0}", hd.MaHoadon).ToList();
					ds.Add(hoadonchitiet);
				}
				return View(ds);
			}
			return RedirectToAction("Index", "PageNotFound");

		}

		[HttpGet]
		public IActionResult PartialBill(int trangthai = 5)
		{
			if (HttpContext.Session.GetString("Username") is not null)
			{
				// Lấy mã người dùng
				var username = HttpContext.Session.GetString("Username");
				var password = HttpContext.Session.GetString("Password");
				Nguoidung nguoidung = _context.Nguoidungs.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
				int manguoidung = nguoidung.MaNguoidung;
				// Lấy các hóa đơn của người dùng
				List<Hoadonchitiet> ds = new List<Hoadonchitiet>();
				List<Hoadon> danhsachhdnd = new List<Hoadon>();
				if (trangthai > 3)
				{
					danhsachhdnd = _context.Hoadons.Where(hd => hd.MaKhachhang == manguoidung).ToList();
				}
				else
				{
					danhsachhdnd = _context.Hoadons.Where(hd => hd.MaKhachhang == manguoidung && hd.TrangthaiHD == trangthai).ToList();
				}
				foreach (var hd in danhsachhdnd)
				{
					Hoadonchitiet hoadonchitiet = new Hoadonchitiet();
					hoadonchitiet.mahoadon = hd.MaHoadon;
					hoadonchitiet.diachi = hd.Diachigiaohang;
					hoadonchitiet.ngaydathang = hd.Ngaydathang?.ToString("dd-MM-yyyy");
					hoadonchitiet.phuongthucthanhtoan = hd.PhuongthucTt;
					hoadonchitiet.trangthai = exchangeStatus(hd.TrangthaiHD);
					// Thêm sản phẩm vào hóa dơn
					hoadonchitiet.danhsachsphd = _context.SanphamHoadons.FromSqlRaw("GetSanphamhoadon {0}", hd.MaHoadon).ToList();
					ds.Add(hoadonchitiet);
				}
				return PartialView("PartialBill", ds);
			}
			return RedirectToAction("Index", "PageNotFound");

		}

		public IActionResult AddComment(string mahoadon)
		{
			if (HttpContext.Session.GetString("Username") is not null)
			{
				Hoadon hd = new Hoadon();
				hd = _context.Hoadons.Where(hd => hd.MaHoadon == mahoadon).FirstOrDefault();

				Hoadonchitiet hoadonchitiet = new Hoadonchitiet();
				hoadonchitiet.mahoadon = hd.MaHoadon;
				hoadonchitiet.makhachhang = hd.MaKhachhang;
				hoadonchitiet.diachi = hd.Diachigiaohang;
				hoadonchitiet.ngaydathang = hd.Ngaydathang?.ToString("dd-MM-yyyy");
				hoadonchitiet.phuongthucthanhtoan = hd.PhuongthucTt;
				hoadonchitiet.trangthai = exchangeStatus(hd.TrangthaiHD);
				// Thêm sản phẩm vào hóa dơn
				hoadonchitiet.danhsachsphd = _context.SanphamHoadons.FromSqlRaw("GetSanphamhoadon {0}", hd.MaHoadon).ToList();

				return View(hoadonchitiet);
			}
			return RedirectToAction("Index", "PageNotFound");

		}
		[HttpGet]
		public IActionResult AddCommentToDB(string masanpham, string comment, int numstar, int makhachhang)
		{
			if (HttpContext.Session.GetString("Username") is not null)
			{
				DanhgiaSp danhgiaSp = new DanhgiaSp();
				danhgiaSp.MaSanpham = masanpham;
				danhgiaSp.MaNguoidung = makhachhang;
				danhgiaSp.Danhgia = comment;
				danhgiaSp.Sao = numstar;
				_context.DanhgiaSps.Add(danhgiaSp);
				_context.SaveChanges();
				return Json(new { status = "success" });
			}
			return RedirectToAction("Index", "PageNotFound");

		}
		public string exchangeStatus(int? numstatus)
		{
			string status = "";
			switch (numstatus)
			{
				case 0: status = "Chờ xử lý"; break;
				case 1: status = "Đang giao"; break;
				case 2: status = "Giao thành công"; break;
				case 3: status = "Hủy"; break;
			}
			return status;
		}
	}
}
