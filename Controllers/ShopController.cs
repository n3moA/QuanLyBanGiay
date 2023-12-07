using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User;
using QuanLyBanGiay_ADMIN.Models_User.ViewModels;
using QuanLyBanGiay_ADMIN.ModelsDAO;
using System.Diagnostics;
using System.Drawing;
using System.Security.Policy;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace QuanLyBanGiay_ADMIN.Controllers
{
	public class ShopController : Controller
	{
		private readonly BanGiayContext _context;


		public ShopController(ILogger<HomeController> logger, BanGiayContext context)
		{
			_context = context;
		}

		public IActionResult Index(int productPage = 1, int size = 12, string MaLoai = "", string searchstring = "", string hangsanxuat = "", string mausac = "", string giaca = "")
		{
			var page = GetProduct(productPage, size, MaLoai, searchstring, hangsanxuat, mausac, giaca);
			ViewBag.currentpage = productPage;
			ViewBag.numpage = page.PageCount;
			return View(page);
		}

		public IPagedList<SanphamHienthi> GetProduct(int productPage, int size, string MaLoai, string searchstring, string hangsanxuat, string mausac, string giaca)
		{
			SanphamhienthiDAO sanphamhienthi = new SanphamhienthiDAO(_context);

			List<SanphamHienthi> sphienthi = sanphamhienthi.GetAll();

			if (MaLoai is null) MaLoai = "";
			if (searchstring is null) searchstring = "";
			if (hangsanxuat is null) hangsanxuat = "";
			if (mausac is null) mausac = "";
			if (giaca is null) giaca = "";

			ViewBag.Maloai = MaLoai;
			ViewBag.Searchstring = searchstring;
			ViewBag.hangsanxuat = hangsanxuat;
			ViewBag.mausac = mausac;
			ViewBag.giaca = giaca;

			if (searchstring != "")
			{
				searchstring = searchstring.ToLower();
				sphienthi = sphienthi.Where(s => changeMG(s.LoaiGiay).Contains(searchstring) || s.TenSanpham.ToLower().Contains(searchstring) || s.Masanpham.ToLower().Contains(searchstring)).ToList();
			}

			if (MaLoai != "")
			{
				sphienthi = sphienthi.Where(s => MaLoai.Contains(s.LoaiGiay)).ToList();
			}

			if (hangsanxuat != "")
			{
				sphienthi = sphienthi.Where(s => hangsanxuat.Contains(s.Hangsanxuat)).ToList();
			}

			if (mausac != "")
			{
				sphienthi = sphienthi.Where(s => mausac.Contains(s.Mausac)).ToList();
			}

			if (giaca != "")
			{
				var gia = giaca.Split('-');
				int min = int.Parse(gia[0]);
				int max = int.Parse(gia[1]);
				sphienthi = sphienthi.Where(s => (((decimal)1 - (decimal)s.Tilegiamgia / 100) * (decimal)s.Giagoc) >= min && (((decimal)1 - (decimal)s.Tilegiamgia / 100) * (decimal)s.Giagoc) <= max).ToList();
			}

			IPagedList<SanphamHienthi> sanphamHienthis = sphienthi.ToPagedList(productPage, size);
			return sanphamHienthis;
		}

		[HttpGet]
		public IActionResult PartialShopIndex(int productPage = 1, int size = 12, string MaLoai = "", string searchstring = "", string hangsanxuat = "", string mausac = "", string giaca = "")
		{
			var page = GetProduct(productPage, size, MaLoai, searchstring, hangsanxuat, mausac, giaca);
			ViewBag.currentpage = productPage;
			ViewBag.numpage = page.PageCount;
			return PartialView("PartialShopIndex", page);
		}

		[HttpGet]
		public IActionResult AddProduct(string id)
		{
			if (id == null || _context.Nhasanxuats == null)
			{
				return NotFound();
			}
			// Lấy ra sản phẩm theo id
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			var sp = from sanpham in sanphams
					 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
					 where sanpham.Ma_Sanpham == id
					 select new
					 {
						 Masanpham = sanpham.Ma_Sanpham,
						 TenSanpham = sanpham.Ten_Sanpham,
						 Giagoc = sanpham.Giagoc,
						 Link1 = anh.Link1,
						 Link2 = anh.Link2,
						 Link3 = anh.Link3,
					 };
			var singleproduct = sp.FirstOrDefault();
			SanphamDetail spdt = new SanphamDetail
			{
				Masanpham = singleproduct.Masanpham,
				TenSanpham = singleproduct.TenSanpham,
				Giagoc = singleproduct.Giagoc,
				link1 = singleproduct.Link1,
				link2 = singleproduct.Link2,
				link3 = singleproduct.Link3,
				// Lấy size, số lượng
				sanPhamSizes = _context.SanPham_Sizes.Where(s => s.MaSanpham == singleproduct.Masanpham).ToList(),
				starmark = 1
			};
			return PartialView("Chooseproduct", spdt);
		}


		public IActionResult ShopProductDetail(string id)
		{
			if (id == null || _context.Nhasanxuats == null)
			{
				return NotFound();
			}
			// Lấy ra sản phẩm theo id
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			var sp = from sanpham in sanphams
					 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
					 where sanpham.Ma_Sanpham == id
					 select new
					 {
						 Masanpham = sanpham.Ma_Sanpham,
						 TenSanpham = sanpham.Ten_Sanpham,
						 Hangsanxuat = sanpham.MaNhasanxuat,
						 Giagoc = sanpham.Giagoc,
						 Link1 = anh.Link1,
						 Link2 = anh.Link2,
						 Link3 = anh.Link3,
					 };
			List<SanphamDetail> dsht = new List<SanphamDetail>();
			foreach (var item in sp)
			{
				SanphamDetail spht = new SanphamDetail();
				spht.Masanpham = item.Masanpham;
				spht.TenSanpham = item.TenSanpham;
				spht.Hangsanxuat = item.Hangsanxuat;
				spht.Giagoc = item.Giagoc;
				spht.link1 = item.Link1;
				spht.link2 = item.Link2;
				spht.link3 = item.Link3;
				// Lấy size, số lượng
				spht.sanPhamSizes = _context.SanPham_Sizes.Where(s => s.MaSanpham == spht.Masanpham).ToList();
				spht.starmark = 1;
				dsht.Add(spht);
			}
			// Sản phẩm tương tự
			string hangsx = id.Substring(0, 3);
			SanphamhienthiDAO sanphamhienthi = new SanphamhienthiDAO(_context);
			List<SanphamHienthi> ds = sanphamhienthi.GetAll();
			ds = ds.Where(e => e.Hangsanxuat == hangsx).ToList();
			ViewBag.Danhsachsp = ds;
			// Load đánh giá sản phẩm
			ViewBag.Danhgiasp = _context.DanhgiaSps.Where(dg => dg.MaSanpham == id).ToList();
			ViewBag.Motasp = _context.MotaSps.FirstOrDefault(m => m.MaSanpham == id);
			ViewBag.user = HttpContext.Session.GetString("Username");
			if (HttpContext.Session.GetString("Username") is not null)
			{
				ViewBag.Makhachhang = _context.Nguoidungs.FirstOrDefault(e => e.Username == HttpContext.Session.GetString("Username")).MaNguoidung;
			}
			return View(dsht);
		}

		public IActionResult PartialProductDetail(int id)
		{
			return PartialView();
		}
		public string changeMG(string mg)
		{
			string tenloaigiay = "";
			switch (mg)
			{
				case "FBS":
					tenloaigiay = "giày đá bóng";
					break;
				case "FNS":
					tenloaigiay = "giày thời trang";
					break;
				case "JGS":
					tenloaigiay = "giày đi bộ/ giày chạy bộ";
					break;
				case "VBS":
					tenloaigiay = "giày bóng chuyền/ giày cầu lông/ giày bóng rổ";
					break;
			}
			return tenloaigiay;
		}
	}
}
