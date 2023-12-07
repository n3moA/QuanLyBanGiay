using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using System.Data;
using Xceed.Wpf.Toolkit;

namespace QuanLyBanGiay_ADMIN.Controllers
{
    public class AdminController : Controller
    {
        private readonly BanGiayContext context;

        //Employees, Departments : dataset trong sql

        //employees, departments: đối tượng kiểu list
        public AdminController(BanGiayContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("tk") is not null)
            {
				var data = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
				ViewBag.YC = data;
				ViewBag.TB = data.Count();

				var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
				ViewBag.CXL = cxl.Count();

				var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
				ViewBag.DXL = dxl.Count();

				var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
				ViewBag.DG = dg.Count();

				var ds = context.Nguoidungs.ToList();
				List<Nguoidung> dskh = new List<Nguoidung>();
				dskh = ds;
				var nd = dskh.Where(x => x.MaChucvu == 2).ToList();
				ViewBag.SoKH = nd.Count();

				var vc = context.Discounts.ToList();
				ViewBag.S = vc.Count();

				var username = HttpContext.Session.GetString("tk");
				ViewBag.Username = username;
				return View();
			}
			return RedirectToAction("Index", "PageNotFound");
		}

       
        public IActionResult DangKy()
        {
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> DangKy(Nguoidung ngd)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                List<Nguoidung> data = context.Nguoidungs.ToList();

                foreach (var tk in data)
                {
                    if (ngd.Username == tk.Username)
                    {
                        await Response.WriteAsync("<script>alert('USERNAME existed');window.location = 'DangKy';</script>");
                        return View();
                    }
                }
                await context.Nguoidungs.AddAsync(ngd);
                await context.SaveChangesAsync();
                await Response.WriteAsync("<script>alert('Created Successfully !!!');window.location.assign('../Login/Index');</script>"); 
                return RedirectToAction("Index", "Login"); 
                
            }
        }

        
    }
}
