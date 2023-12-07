using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using System.Security.Cryptography.Xml;
using X.PagedList;

namespace QuanLyBanGiay_ADMIN.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly BanGiayContext context;

        public NguoiDungController(BanGiayContext context)
        {
            this.context = context;
        }
        public IActionResult Index(string TenNguoidung, int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.TB = da.Count();
            ViewBag.YC = da.ToList();

            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            if (TenNguoidung == null)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                var data = context.ThongTinNguoiDungs.FromSqlRaw("Exec ThongTin_NguoiDung").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            else
            {
                ViewBag.TenND = TenNguoidung.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                var data = context.TimKiem_NguoiDung(TenNguoidung).ToList().ToPagedList(page, pagesize);
                return View(data);
            }
        }

        [HttpGet]
        public IActionResult UpdateNguoiDung(int MaNguoidung) 
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            List<ThongTinNguoiDung> thongtins = new List<ThongTinNguoiDung>();
            var data = context.ThongTinNguoiDungs.FromSqlRaw("Exec ThongTin_NguoiDung").ToList();
            thongtins = data;

            ThongTinNguoiDung? MaId = thongtins.Find(
                (ThongTinNguoiDung ob) => { return (ob.MaNguoidung == MaNguoidung); }    //tìm đối tượng trong list
            );

            var data_dep = context.ChucVus.ToList();
            ViewBag.depar_data = data_dep;

            if ( MaId != null )
            {
                var tt = new ThongTinNguoiDung()
                {
                    MaNguoidung = MaId.MaNguoidung,
                    Username = MaId.Username,
                    Password = MaId.Password,
                    TenNguoidung = MaId.TenNguoidung,
                    Gioitinh = MaId.Gioitinh,
                    Chucvu = MaId.Chucvu,
                    SodienthoaiNv = MaId.SodienthoaiNv,
                    DiachiNv = MaId.DiachiNv,
                    MaChucvu = MaId.MaChucvu,
                };
                return View(tt);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> UpdateNguoiDung(Nguoidung emp)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var employeeId = await context.Nguoidungs.FindAsync(emp.MaNguoidung);
            if (employeeId != null)
            {
                employeeId.MaNguoidung = emp.MaNguoidung;
                employeeId.Username = emp.Username;
                employeeId.Password = emp.Password;
                employeeId.TenNguoidung = emp.TenNguoidung;
                employeeId.Gioitinh = emp.Gioitinh;
                employeeId.MaChucvu = emp.MaChucvu;
                
                employeeId.DiachiNv = emp.DiachiNv;

                if (emp.SodienthoaiNv == null)
                {
                    await Response.WriteAsync("<script>alert('Check Again !!!');window.location = 'UpdateNguoiDung';</script>");
                    return View();
                }
                else
                {
                    employeeId.SodienthoaiNv = emp.SodienthoaiNv;
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                
            }

            return View();
        }

        public async Task<IActionResult> DeleteNguoiDung(int MaNguoidung)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var delEmp = await context.Nguoidungs.FindAsync(MaNguoidung);
            if (delEmp != null)
            {

                await context.Database.ExecuteSqlRawAsync("EXEC Xoa_NguoiDung {0}", MaNguoidung);
                await Response.WriteAsync("<script>alert('Delete Successfully !!!');window.location = 'Index';</script>");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult ADDNguoiDung()
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var data = context.ChucVus.ToList();
            ViewBag.depar_data = data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ADDNguoiDung(Nguoidung ngd)
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
                        await Response.WriteAsync("<script>alert('USERNAME existed');window.location = 'ADDNguoiDung';</script>");
                        return View();
                    }
                }
                await context.Nguoidungs.AddAsync(ngd);
                await context.SaveChangesAsync();
                await Response.WriteAsync("<script>alert('Created Successfully !!!');window.location.assign('../NguoiDung/Index');</script>");
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public IActionResult LichSu(int Ma_Nguoidung)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var data = context.LichSuMuaHangs.FromSqlRaw("EXEC LichSuMH {0}",Ma_Nguoidung).ToList();

            var data1 = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            List<HienThiHoaDon> hd = new List<HienThiHoaDon>();
            hd = data1;

            var tt = hd.Where(x=>x.MaKhachhang==Ma_Nguoidung).FirstOrDefault();
            ViewBag.ThongTin = tt;

            return View(data);
        }


    }
}
