using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Globbing;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using X.PagedList;
using Xceed.Wpf.Toolkit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuanLyBanGiay_ADMIN.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly BanGiayContext context;

        public SanPhamController(BanGiayContext context)
        {
            this.context = context;
        }

        //public IActionResult Index(string Ten_Nhasanxuat,string Ten_Sanpham, int page = 1)
        //{
        //    if (Ten_Nhasanxuat == null&&Ten_Sanpham == null)
        //    {
        //        ViewBag.TenNSX = null;
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    else if (Ten_Nhasanxuat == null && Ten_Sanpham != null)
        //    {
        //        ViewBag.TenNSX = null;
        //        ViewBag.TenSP = Ten_Sanpham.ToString();
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.TimTen_All(Ten_Sanpham).ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    else if (Ten_Nhasanxuat == "NIKE" && Ten_Sanpham == null)
        //    {
        //        ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.hienThiSanPhams(Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    else if (Ten_Nhasanxuat == "Adidas" && Ten_Sanpham == null)
        //    {
        //        ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.hienThiSanPhams(Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    else if (Ten_Nhasanxuat == "Mizuno" && Ten_Sanpham == null)
        //    {
        //        ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.hienThiSanPhams(Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    else if (Ten_Nhasanxuat == "BitisHunter" && Ten_Sanpham == null)
        //    {
        //        ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.hienThiSanPhams(Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    else if (Ten_Nhasanxuat != null && Ten_Sanpham != null)
        //    {
        //        ViewBag.TenSP = Ten_Sanpham.ToString();
        //        ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
        //        page = page < 1 ? 1 : page;
        //        int pagesize = 8;
        //        var data = context.TimTen_NSX(Ten_Nhasanxuat,Ten_Sanpham).ToList().ToPagedList(page, pagesize);
        //        return View(data);
        //    }
        //    return View();

        //}

        public IActionResult Index(string Ten_Nhasanxuat, string Ten_loai, string dieukien, string size, int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            ViewBag.TB = da.Count();
            
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();

            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;

            if (Ten_Nhasanxuat == null && Ten_loai == null && dieukien == null && size == null)
            {
                ViewBag.TenNSX = null;
                ViewBag.TenLoai = null;
                ViewBag.Dieukien = null;
                ViewBag.Size = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            if (Ten_Nhasanxuat != null && Ten_loai != null && dieukien != null && size != null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = Ten_loai.ToString();
                ViewBag.Size = size.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                var filterLoai = filterNSX.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);

                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);

                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);

                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
            }
            if (Ten_Nhasanxuat != null && Ten_loai == null && dieukien == null && size == null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = null;
                ViewBag.Dieukien = null;
                ViewBag.Size = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                return View(filterNSX);

            }

            if (Ten_Nhasanxuat == null && Ten_loai != null && dieukien == null && size == null)
            {
                ViewBag.TenNSX = null;
                ViewBag.TenLoai = Ten_loai;
                ViewBag.Dieukien = null;
                ViewBag.Size = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterLoai = product.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                return View(filterLoai);

            }


            
            if (Ten_Nhasanxuat != null && Ten_loai != null && dieukien == null && size == null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = Ten_loai.ToString();
                ViewBag.Dieukien = null;
                ViewBag.Size = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                var filterLoai = filterNSX.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                return View(filterLoai);

            }

            if (Ten_Nhasanxuat != null && Ten_loai == null && dieukien != null && size == null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = null;
                ViewBag.Size = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterNSX.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterNSX.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterNSX.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
            }

            if (Ten_Nhasanxuat == null && Ten_loai != null && dieukien != null && size == null)
            {
                ViewBag.TenNSX = null;
                ViewBag.Size = null;
                ViewBag.TenLoai = Ten_loai.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterLoai = product.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
            }

            if (Ten_Nhasanxuat != null && Ten_loai == null && dieukien == null && size != null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = null;
                ViewBag.Dieukien = null;
                ViewBag.Size = size.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                var filterSize = filterNSX.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                return View(filterSize);
            }

            if (Ten_Nhasanxuat == null && Ten_loai != null && dieukien == null && size != null)
            {
                ViewBag.TenNSX = null;
                ViewBag.TenLoai = Ten_loai.ToString();
                ViewBag.Dieukien = null;
                ViewBag.Size = size.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterLoai = product.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                var filterSize = filterLoai.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                return View(filterSize);
            }

            if (Ten_Nhasanxuat == null && Ten_loai == null && dieukien != null && size == null)
            {
                ViewBag.TenNSX = null;
                ViewBag.TenLoai = null;
                ViewBag.Size = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = product.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = product.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = product.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
            }

            if (Ten_Nhasanxuat == null && Ten_loai == null && dieukien == null && size != null)
            {
                ViewBag.TenNSX = null;
                ViewBag.TenLoai = null;
                ViewBag.Dieukien = null;
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                ViewBag.Size = size.ToString();
                var filterSize = product.Where(x =>x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                return View(filterSize);
            }


            if (Ten_Nhasanxuat == null && Ten_loai == null && dieukien != null && size != null)
            {
                ViewBag.TenNSX = null;
                ViewBag.TenLoai = null;

                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = product.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    ViewBag.Size = size.ToString();
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = product.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    ViewBag.Size = size.ToString();
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = product.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    ViewBag.Size = size.ToString();
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
            }

            if (Ten_Nhasanxuat != null && Ten_loai != null && dieukien == null && size != null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = Ten_loai.ToString();
                ViewBag.Dieukien = null;
                ViewBag.Size = size.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                var filterLoai = filterNSX.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                var filterSize = filterLoai.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                return View(filterSize);
            }

            if (Ten_Nhasanxuat != null && Ten_loai == null && dieukien != null && size != null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.TenLoai = null;
                ViewBag.Size = size.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterNSX.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterNSX.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterNSX.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
            }

            if (Ten_Nhasanxuat == null && Ten_loai != null && dieukien != null && size != null)
            {
                ViewBag.TenNSX = null;
                ViewBag.Size = size.ToString();
                ViewBag.TenLoai = Ten_loai.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterLoai = product.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    var filterSize = filterGia.Where(x => x.All_Size.Contains(size)).ToList().ToPagedList(page, pagesize);
                    return View(filterSize);
                }
            }

            if (Ten_Nhasanxuat != null && Ten_loai != null && dieukien != null && size == null)
            {
                ViewBag.TenNSX = Ten_Nhasanxuat.ToString();
                ViewBag.Size = null;
                ViewBag.TenLoai = Ten_loai.ToString();
                page = page < 1 ? 1 : page;
                int pagesize = 8;
                List<HienThiSanPham> product = new List<HienThiSanPham>();
                var data = context.HienThiSanPhams.FromSqlRaw("HienThiSanPham").ToList();
                product = data;
                var filterNSX = product.Where(x => x.Ten_Nhasanxuat == Ten_Nhasanxuat).ToList().ToPagedList(page, pagesize);
                var filterLoai = filterNSX.Where(x => x.Ten_loai == Ten_loai).ToList().ToPagedList(page, pagesize);
                if (dieukien == "1")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc < 1000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "2")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc >= 1000000 && x.Giagoc <= 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
                if (dieukien == "3")
                {
                    ViewBag.Dieukien = dieukien.ToString();
                    var filterGia = filterLoai.Where(x => x.Giagoc > 3000000).ToList().ToPagedList(page, pagesize);
                    return View(filterGia);
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Details(string MaSanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            List<ChiTietSanPham> ct = new List<ChiTietSanPham>();
            var data = context.ChiTietSanPhams.FromSqlRaw("ChiTietSanPham").ToList();
            ct = data;

            ChiTietSanPham? MaId = ct.Find(
                (ChiTietSanPham ob) => { return (ob.MaSanpham == MaSanpham); }    //tìm đối tượng trong list
            );

            var data_LoaiGiay = context.LoaiGiays.ToList();
            ViewBag.LG_data = data_LoaiGiay;

            var data_Hangsx = context.Nhasanxuats.ToList();
            ViewBag.NSX_data = data_Hangsx;

            var size_data = context.Size_theoSanPham(MaSanpham).ToList();
            ViewBag.BangSize = size_data;

            if (MaId != null)
            {
                var sp = new ChiTietSanPham()
                {
                    MaSanpham = MaId.MaSanpham,
                    Ten_Sanpham = MaId.Ten_Sanpham,
                    Mausac = MaId.Mausac,
                    MaNhasanxuat = MaId.MaNhasanxuat,
                    Ten_Nhasanxuat = MaId.Ten_Nhasanxuat,
                    MaLoai = MaId.MaLoai,
                    Ten_loai = MaId.Ten_loai,
                    Link1 = MaId.Link1,
                    Link2 = MaId.Link2,
                    Link3 = MaId.Link3,
                    Giagoc = MaId.Giagoc
                };
                return View(sp);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Details(Sanpham emp)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var employeeId = await context.Sanphams.FindAsync(emp.Ma_Sanpham);
            if (employeeId != null)
            {
                employeeId.Ma_Sanpham = emp.Ma_Sanpham;
                employeeId.Ten_Sanpham = emp.Ten_Sanpham;
                employeeId.Mausac = emp.Mausac;
                employeeId.MaLoai = emp.MaLoai;
                employeeId.MaNhasanxuat = emp.MaNhasanxuat;
                employeeId.Giagoc = emp.Giagoc;


                await context.SaveChangesAsync();
                await Response.WriteAsync("<script>alert('UPDATE SUCCESSFULLY !');window.location = 'Index';</script>");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> UpDateHinhAnh(string MaSanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var sp_anh = await context.AnhSps.FindAsync(MaSanpham);
            if (sp_anh != null)
            {
                var sp = new AnhSp()
                {
                    MaSanpham = sp_anh.MaSanpham,
                    Link1 = sp_anh.Link1,
                    Link2 = sp_anh.Link2,
                    Link3 = sp_anh.Link3
                };
                return View(sp);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> UpDateHinhAnh(AnhSp New_Pic)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var dep = await context.AnhSps.FindAsync(New_Pic.MaSanpham);
            if (dep != null)
            {
                dep.MaSanpham = New_Pic.MaSanpham;
                dep.Link1 = New_Pic.Link1;
                dep.Link2 = New_Pic.Link2;
                dep.Link3 = New_Pic.Link3;

                await context.SaveChangesAsync();
                //  await Response.WriteAsync("<script>alert('UPDATE SUCCESSFULLY !')</script>");
                return RedirectToAction("Details", "SanPham", new { MaSanpham = dep.MaSanpham });
            }
            return RedirectToAction("Details", "SanPham", new { MaSanpham = dep.MaSanpham });
        }

        [HttpGet]

        public async Task<IActionResult> Size_SoLuong(string Ma_Sanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var dataLinQ = from dat in context.SanPham_Sizes
                           join spham in context.Sanphams on dat.MaSanpham equals spham.Ma_Sanpham
                           join asp in context.AnhSps on spham.Ma_Sanpham equals asp.MaSanpham
                           where dat.MaSanpham == Ma_Sanpham
                           select new
                           {
                               Ma_Sanpham = spham.Ma_Sanpham,
                               Ten_Sanpham = spham.Ten_Sanpham,
                               Link1 = asp.Link1,
                               Link2 = asp.Link2,
                               Link3 = asp.Link3,
                               Ma_Size = dat.Ma_Size,
                               SoLuong = dat.SoLuong
                           };

            List<Size_theoSanPham> data = new List<Size_theoSanPham>();
            foreach (var item in dataLinQ)
            {
                Size_theoSanPham sp = new Size_theoSanPham();
                sp.MaSanpham = item.Ma_Sanpham;
                sp.Ten_Sanpham = item.Ten_Sanpham;
                sp.Link1 = item.Link1;
                sp.Link2 = item.Link2;
                sp.Link3 = item.Link3;
                sp.Ma_Size = item.Ma_Size;
                sp.SoLuong = item.SoLuong;
                data.Add(sp);
            }
            //var data = context.LocSize_theoSanPham(Ma_Sanpham).ToList();
            ViewBag.MaSanpham = Ma_Sanpham;
            return View(data);
        }

        [HttpGet]
        public IActionResult UpDateSize_SoLuong(string Ma_Size, string Ma_Sanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var dataLinQ = from dat in context.SanPham_Sizes
                           join spham in context.Sanphams on dat.MaSanpham equals spham.Ma_Sanpham
                           join asp in context.AnhSps on spham.Ma_Sanpham equals asp.MaSanpham
                           select new
                           {
                               Ma_Sanpham = spham.Ma_Sanpham,
                               Ten_Sanpham = spham.Ten_Sanpham,
                               Link1 = asp.Link1,
                               Link2 = asp.Link2,
                               Link3 = asp.Link3,
                               Ma_Size = dat.Ma_Size,
                               SoLuong = dat.SoLuong
                           };

            List<Size_theoSanPham> data = new List<Size_theoSanPham>();
            foreach (var item in dataLinQ)
            {
                Size_theoSanPham sp = new Size_theoSanPham();
                sp.MaSanpham = item.Ma_Sanpham;
                sp.Ten_Sanpham = item.Ten_Sanpham;
                sp.Link1 = item.Link1;
                sp.Link2 = item.Link2;
                sp.Link3 = item.Link3;
                sp.Ma_Size = item.Ma_Size;
                sp.SoLuong = item.SoLuong;
                data.Add(sp);
            }

            Size_theoSanPham? MaId = data.Find(
                (Size_theoSanPham ob) => { return (ob.MaSanpham == Ma_Sanpham && ob.Ma_Size == Ma_Size); }    //tìm đối tượng trong list
            );

            if (MaId != null)
            {
                var sp = new Size_theoSanPham()
                {
                    MaSanpham = MaId.MaSanpham,
                    Ten_Sanpham = MaId.Ten_Sanpham,
                    Link1 = MaId.Link1,
                    Link2 = MaId.Link2,
                    Link3 = MaId.Link3,
                    Ma_Size = MaId.Ma_Size,
                    SoLuong = MaId.SoLuong,
                };
                return View(sp);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> UpDateSize_SoLuong(SanPham_Size sp_s)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var data = await context.SanPham_Sizes.FindAsync(sp_s.MaSanpham, sp_s.Ma_Size);
            if (data != null)
            {
                data.MaSanpham = sp_s.MaSanpham;
                data.Ma_Size = sp_s.Ma_Size;
                data.SoLuong = sp_s.SoLuong;

                await context.SaveChangesAsync();
                //await Response.WriteAsync("<script>alert('UPDATE SUCCESSFULLY !');window.location = 'Index';</script>");
                return RedirectToAction("Details", "SanPham", new { MaSanpham = data.MaSanpham });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> ThemSize(string Ma_Sanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            List<ChiTietSanPham> ct = new List<ChiTietSanPham>();
            var data = context.ChiTietSanPhams.FromSqlRaw("ChiTietSanPham").ToList();
            ct = data;

            ChiTietSanPham? sp_anh = ct.Find(
                (ChiTietSanPham ob) => { return (ob.MaSanpham == Ma_Sanpham); }    //tìm đối tượng trong list
            );
            var new_size = context.ThemSize_Moi(Ma_Sanpham).ToList();
            ViewBag.Size_Moi = new_size;

            if (sp_anh != null)
            {
                var sp = new ChiTietSanPham()
                {
                    MaSanpham = sp_anh.MaSanpham,
                    Ten_Sanpham = sp_anh.Ten_Sanpham,
                    Mausac = sp_anh.Mausac,
                    MaNhasanxuat = sp_anh.MaNhasanxuat,
                    Ten_Nhasanxuat = sp_anh.Ten_Nhasanxuat,
                    MaLoai = sp_anh.MaLoai,
                    Ten_loai = sp_anh.Ten_loai,
                    Link1 = sp_anh.Link1,
                    Link2 = sp_anh.Link2,
                    Link3 = sp_anh.Link3,
                    Giagoc = sp_anh.Giagoc
                };
                return View(sp);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ThemSize(SanPham_Size ngd)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {

                await context.SanPham_Sizes.AddAsync(ngd);
                await context.SaveChangesAsync();
                // await Response.WriteAsync("<script>alert('Created Successfully !!!');</script>");
                return RedirectToAction("Details", "SanPham", new { MaSanpham = ngd.MaSanpham });
            }
        }

        public async Task<IActionResult> DeleteSize(string MaSanpham, string Ma_Size)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var delEmp = await context.SanPham_Sizes.FindAsync(MaSanpham, Ma_Size);
            if (delEmp != null)
            {
                //context.Remove(delDep);
                //await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("EXEC Xoa_Size {0},{1}", MaSanpham, Ma_Size);
                // await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/Index');</script>");
                return RedirectToAction("Details", "SanPham", new { MaSanpham = MaSanpham });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSanPham(string MaSanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var delEmp = await context.Sanphams.FindAsync(MaSanpham);
            if (delEmp != null)
            {
                //context.Remove(delDep);
                //await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("EXEC Xoa_SanPham_proc {0}", MaSanpham);
                await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/Index');</script>");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult ThungRac(int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            page = page < 1 ? 1 : page;
            int pagesize = 8;
            var data = context.Sanphams.FromSqlRaw("EXEC ThungRac_proc").ToList().ToPagedList(page, pagesize);
            return View(data);
        }


        public async Task<IActionResult> KhoiPhuc(string MaSanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var data = context.Sanphams.FromSqlRaw("EXEC ThungRac_proc").ToList();
            List<Sanpham> sp = new List<Sanpham>();
            sp = data;

            Sanpham? sp_anh = sp.Find(
               (Sanpham ob) => { return (ob.Ma_Sanpham == MaSanpham); }    //tìm đối tượng trong list
           );

            if (sp_anh != null)
            {
                await context.Database.ExecuteSqlRawAsync("EXEC KhoiPhuc {0}", MaSanpham);
                await context.SaveChangesAsync();
                await Response.WriteAsync("<script>alert('Restore Successfully !!!');window.location.assign('../SanPham/Index');</script>");
                return RedirectToAction("Index", "SanPham");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ADDSanPham()
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var data_LoaiGiay = context.LoaiGiays.ToList();
            ViewBag.LG_data = data_LoaiGiay;

            var data_Hangsx = context.Nhasanxuats.ToList();
            ViewBag.NSX_data = data_Hangsx;

            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ADDSanPham(Sanpham sp)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            if (sp.Ma_Sanpham != null)
            {
                List<Sanpham> data = context.Sanphams.ToList();

                foreach (var tk in data)
                {
                    if (sp.Ma_Sanpham == tk.Ma_Sanpham)
                    {
                        await Response.WriteAsync("<script>alert('Product existed');window.location = 'ADDSanPham';</script>");
                        return View();
                    }
                }
                await context.Sanphams.AddAsync(sp);
                //await context.Database.ExecuteSqlRawAsync("EXEC ThemSP {0},{1},{2},{3},{4},{5}", sp.Ma_Sanpham,sp.Ten_Sanpham,sp.Mausac,sp.MaNhasanxuat,sp.MaLoai,sp.Giagoc);
                await context.SaveChangesAsync();
                return RedirectToAction("UpDateHinhAnh", "SanPham", new { MaSanpham = sp.Ma_Sanpham });
            }
            else
            {
                ViewBag.Error = "Thiếu thông tin !";
                return View();
            }
            
        }

        public async Task<IActionResult> DeleteThungRac(string Ma_Sanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var delEmp = await context.ThungRacs.FindAsync(Ma_Sanpham);
            if (delEmp != null)
            {
                context.Remove(delEmp);
                await context.SaveChangesAsync();

                //await context.Database.ExecuteSqlRawAsync("EXEC Xoa_SanPham_proc {0}", Ma_Sanpham);
                await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/ThungRac');</script>");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAll()
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            await context.Database.ExecuteSqlRawAsync("EXEC Xoa_All");
            await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/ThungRac');</script>");
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult KhoVoucher()
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();

            ViewBag.TB = da.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            var data = context.Discounts.ToList();
            return View(data);
        }

        public async Task<IActionResult> SanPham_ApDung(int MaDieukien, int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            if (MaDieukien == 1)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                ViewBag.MaDK = MaDieukien.ToString();
                var data = context.ListSanPham_Vouchers.FromSqlRaw("Voucher_SP_hon2cu").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            if (MaDieukien == 2)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                ViewBag.MaDK = MaDieukien.ToString();
                var data = context.ListSanPham_Vouchers.FromSqlRaw("Voucher_SP_hon1cu_Addidas").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            if (MaDieukien == 3)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                ViewBag.MaDK = MaDieukien.ToString();
                var data = context.ListSanPham_Vouchers.FromSqlRaw("Voucher_SP_hon3cu_Nike").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            if (MaDieukien == 4)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                ViewBag.MaDK = MaDieukien.ToString();
                var data = context.ListSanPham_Vouchers.FromSqlRaw("Voucher_SP_Nike_Adidas").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            if (MaDieukien == 5)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                ViewBag.MaDK = MaDieukien.ToString();
                var data = context.ListSanPham_Vouchers.FromSqlRaw("Voucher_SP_ALL").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            if (MaDieukien == 6)
            {
                page = page < 1 ? 1 : page;
                int pagesize = 6;
                ViewBag.MaDK = MaDieukien.ToString();
                var data = context.ListSanPham_Vouchers.FromSqlRaw("Voucher_Miz_All").ToList().ToPagedList(page, pagesize);
                return View(data);
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> ApDung_Voucher(string MaSanpham, int MaDieukien)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            await context.Database.ExecuteSqlRawAsync("EXEC ApDung_Voucher {0},{1}", MaSanpham, MaDieukien);
            await context.SaveChangesAsync();
            await Response.WriteAsync("<script>alert('Applied Successfully !!!');window.location.assign('../SanPham/KhoVoucher');</script>");
            return RedirectToAction("SanPham_ApDung_Voucher", "SanPham");
        }

        [HttpGet]

        public IActionResult SanPham_ApDung_Voucher(int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            ViewBag.TB = da.Count();

            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();
            page = page < 1 ? 1 : page;
            int pagesize = 6;
            var data = context.SanPham_Applieds.FromSqlRaw("SanPham_daApDung_Voucher").ToList().ToPagedList(page, pagesize);
            return View(data);
        }

        public async Task<IActionResult> DeleteVoucher(string MaGiamgia)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var delEmp = await context.Discounts.FindAsync(MaGiamgia);
            if (delEmp != null)
            {
                context.Remove(delEmp);
                await context.SaveChangesAsync();

                await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/KhoVoucher');</script>");
                return RedirectToAction("KhoVoucher", "SanPham");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BoVoucher(string MaSanpham)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            context.Database.ExecuteSqlRaw("EXEC Bo_Voucher {0}", MaSanpham);
            await context.SaveChangesAsync();

            await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/SanPham_ApDung_Voucher');</script>");
            return RedirectToAction("SanPham_ApDung_Voucher", "SanPham");

        }
        public IActionResult HoaDon_ChuaXuLy(int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            ViewBag.TB = da.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            page = page < 1 ? 1 : page;
            int pagesize = 4;
            var data = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList().ToPagedList(page, pagesize);
            return View(data);
        }

        public async Task<IActionResult> HuyHoaDon(string Ma_Hoadon, int MaNguoidung)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            //var delEmp = await context.Hoadons.FindAsync(Ma_Hoadon);
            //if (delEmp != null)
            //{
            //    context.Remove(delEmp);
            //    await context.SaveChangesAsync();

            //    
            //    
            //}
            //return RedirectToAction("Index");
            await context.Database.ExecuteSqlRawAsync("EXEC Huy_HoaDon {0},{1}", Ma_Hoadon, MaNguoidung);
            await context.SaveChangesAsync();
            await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/HoaDon_ChuaXuLy');</script>");
            return RedirectToAction("HoaDon_ChuaXuLy", "SanPham");
        }
        public IActionResult ChiTietHoaDon_ChuaXuLy(string MaHoadon)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();
            ViewBag.TB = da.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            //var data = context.ChiTietHoaDon_ListSPs.FromSqlRaw("ChiTietHoaDon_ChuaXuLy {0}", MaHoadon).ToList();
            //return View(data);
            var dataLinQ = from sp1 in context.Sanphams
                           join di in context.Discounts on sp1.MaGiamgia equals di.MaGiamgia into sp_di
                           from sp in sp_di.DefaultIfEmpty()
                           join asp in context.AnhSps on sp1.Ma_Sanpham equals asp.MaSanpham
                           join ct in context.ChitietHoadons on sp1.Ma_Sanpham equals ct.MaSanpham
                           where ct.MaHoadon == MaHoadon
                           select new
                           {
                               MaHoadon = ct.MaHoadon,
                               MaSanpham = sp1.Ma_Sanpham,
                               Ten_Sanpham = sp1.Ten_Sanpham,
                               Link1 = asp.Link1,
                               Link2 = asp.Link2,
                               Link3 = asp.Link3,
                               size = ct.size,
                               Soluong = ct.Soluong,
                               Giagoc = sp1.Giagoc,
                               MaGiamgia = sp.MaGiamgia,
                               Trangthai = ct.Trangthai,
                               Tilegiamgia = (sp.MaGiamgia == null) ? 0 : sp.Tilegiamgia,
                           };

            List<ChiTietHoaDon_ListSP> data = new List<ChiTietHoaDon_ListSP>();
            foreach (var item in dataLinQ)
            {
                ChiTietHoaDon_ListSP sp = new ChiTietHoaDon_ListSP();
                sp.MaHoadon = item.MaHoadon;
                sp.MaSanpham = item.MaSanpham;
                sp.Ten_Sanpham = item.Ten_Sanpham;
                sp.Link1 = item.Link1;
                sp.Link2 = item.Link2;
                sp.Link3 = item.Link3;
                sp.size = item.size;
                sp.Soluong = item.Soluong;
                sp.Giagoc = item.Giagoc;
                sp.MaGiamgia = item.MaGiamgia;
                sp.Trangthai = item.Trangthai;
                sp.Tilegiamgia = item.Tilegiamgia;
                data.Add(sp);

            }

            var data1 = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            List<HienThiHoaDon> hd = new List<HienThiHoaDon>();
            hd = data1;

            var tt = hd.Where(x => x.MaHoadon == MaHoadon).FirstOrDefault();
            ViewBag.ThongTin = tt;

            return View(data);
        }

        public async Task<IActionResult> XacNhan_SanPham(string MaHoadon, string MaSanpham, int size, int Soluong)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var sanpham = context.SanPham_Sizes.ToList();
            List<SanPham_Size> sp = new List<SanPham_Size>();
            sp = sanpham;

            var sp1 = sp.Where(x => x.MaSanpham == MaSanpham).ToList();
            var kq = sp1.Where(x => x.Ma_Size == size.ToString()).FirstOrDefault();
            ViewBag.SoLuongTon = kq;
            var conlai = kq.SoLuong - Soluong;
            if (conlai >= 0)
            {
                await context.Database.ExecuteSqlRawAsync("EXEC XacNhan_SanPham {0},{1},{2},{3}", MaHoadon, MaSanpham, size, Soluong);


                string chuoiCoDau = "Code " + MaSanpham + " has " + conlai + " products";
                await context.SaveChangesAsync();
                await Response.WriteAsync("<script> window.alert('Successfully !!, " + chuoiCoDau + "');window.location.assign('../SanPham/ChiTietHoaDon_ChuaXuLy?MaHoadon=" + MaHoadon + "'); </script>");

                return RedirectToAction("ChiTietHoaDon_ChuaXuLy", "SanPham", new { MaHoadon = MaHoadon });
            }
            else
            {

                await Response.WriteAsync(
                    "<script>" +
                    " var result = confirm(' Not enough products that only has " + kq.SoLuong + " products !! Update products ?');if(result == false){window.location.assign('../SanPham/ChiTietHoaDon_ChuaXuLy?MaHoadon=" + MaHoadon + "')}else{window.location.assign('../SanPham/CapNhat_SanPham_HoaDon?MaHoadon=" + MaHoadon + "&&MaSanpham=" + MaSanpham + "&&size=" + size + "')}; </script>");

                return RedirectToAction("ChiTietHoaDon_ChuaXuLy", "SanPham", new { MaHoadon = MaHoadon });
            }

        }

        [HttpGet]
        public async Task<IActionResult> CapNhat_SanPham_HoaDon(string MaHoadon, string MaSanpham, int size)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();

            ViewBag.TB = da.Count();
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            var dataLinQ = from sp in context.Sanphams
                           join asp in context.AnhSps on sp.Ma_Sanpham equals asp.MaSanpham
                           join ct in context.ChitietHoadons on sp.Ma_Sanpham equals ct.MaSanpham
                           where ct.MaHoadon == MaHoadon && sp.Ma_Sanpham == MaSanpham
                           select new
                           {
                               MaHoadon = ct.MaHoadon,
                               MaSanpham = sp.Ma_Sanpham,
                               Ten_Sanpham = sp.Ten_Sanpham,
                               Link1 = asp.Link1,
                               Link2 = asp.Link2,
                               Link3 = asp.Link3,
                               size = ct.size,
                               Soluong = ct.Soluong,
                           };

            List<CapNhat_SanPham_trongHoaDon> td = new List<CapNhat_SanPham_trongHoaDon>();
            foreach (var item in dataLinQ)
            {
                CapNhat_SanPham_trongHoaDon sp = new CapNhat_SanPham_trongHoaDon();
                sp.MaHoadon = item.MaHoadon;
                sp.MaSanpham = item.MaSanpham;
                sp.Ten_Sanpham = item.Ten_Sanpham;
                sp.Link1 = item.Link1;
                sp.Link2 = item.Link2;
                sp.Link3 = item.Link3;
                sp.size = item.size;
                sp.Soluong = item.Soluong;
                td.Add(sp);
            }

            var sanpham = context.SanPham_Sizes.ToList();
            List<SanPham_Size> sp1 = new List<SanPham_Size>();
            sp1 = sanpham;

            var sl = sp1.Where(x => x.MaSanpham == MaSanpham).ToList();
            var kq1 = sl.Where(x => x.Ma_Size == size.ToString()).FirstOrDefault();
            ViewBag.SoLuongTon = kq1;

            var data1 = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            List<HienThiHoaDon> hd = new List<HienThiHoaDon>();
            hd = data1;

            var tt = hd.Where(x => x.MaHoadon == MaHoadon).FirstOrDefault();
            ViewBag.ThongTin = tt;

            CapNhat_SanPham_trongHoaDon? kq = td.Find(
               (CapNhat_SanPham_trongHoaDon ob) => { return (ob.size == size); }    //tìm đối tượng trong list
           );

            return View(kq);
        }

        [HttpPost]
        public async Task<IActionResult> CapNhat_SanPham_HoaDon(ChitietHoadon ct)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            if (ct.Soluong == 0)
            {
                await context.Database.ExecuteSqlRawAsync("EXEC XoaSp_TrongHoaDon {0},{1},{2}", ct.MaHoadon, ct.MaSanpham, ct.size);

                await context.SaveChangesAsync();
                await Response.WriteAsync("<script>alert('Saved Successfully !!!');window.location.assign('../SanPham/ChiTietHoaDon_ChuaXuLy?MaHoadon=" + ct.MaHoadon + "');</script>");

                return RedirectToAction("Index");
            }
            else
            {
                var data1 = context.ChitietHoadons.ToList();
                List<ChitietHoadon> da1 = new List<ChitietHoadon>();
                da1 = data1;

                ChitietHoadon? data = da1.Find(
                  (ChitietHoadon ob) => { return (ob.MaHoadon == ct.MaHoadon && ob.MaSanpham == ct.MaSanpham && ob.size == ct.size); }    //tìm đối tượng trong list
              );

                if (data != null)
                {
                    data.MaHoadon = ct.MaHoadon;
                    data.MaSanpham = ct.MaSanpham;
                    data.size = ct.size;
                    data.Soluong = ct.Soluong;
                    data.Trangthai = ct.Trangthai;

                    await context.SaveChangesAsync();
                    await Response.WriteAsync("<script>alert('UPDATE SUCCESSFULLY !');window.location.assign('../SanPham/ChiTietHoaDon_ChuaXuLy?MaHoadon=" + ct.MaHoadon + "');</script>");

                }
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteSanPham_HoaDon(string MaHoadon,string MaSanpham,int size)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            await context.Database.ExecuteSqlRawAsync("EXEC XoaSp_TrongHoaDon {0},{1},{2}", MaHoadon, MaSanpham, size);

            await context.SaveChangesAsync();
            await Response.WriteAsync("<script>alert('Deleted Successfully !!!');window.location.assign('../SanPham/ChiTietHoaDon_ChuaXuLy?MaHoadon=" + MaHoadon + "');</script>");
                
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> XacNhan_HoaDon(string MaHoadon, int MaKhachhang)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            await context.Database.ExecuteSqlRawAsync("EXEC XacNhanHoaDon {0},{1}", MaKhachhang, MaHoadon);
            await context.SaveChangesAsync();
            return RedirectToAction("HoaDon_ChuaXuLy", "SanPham");
        }
        public IActionResult HoaDon_DaXuLy(int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();

            ViewBag.TB = da.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            page = page < 1 ? 1 : page;
            int pagesize = 4;
            var data = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList().ToPagedList(page, pagesize);
            return View(data);
        }

        public async Task<IActionResult> XacNhanGiaoHang(string MaHoadon, int MaKhachhang)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            var ycau = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            List<HienThiYeuCau> ycs = new List<HienThiYeuCau>();
            ycs = ycau;

            var tontaihd = ycs.Where(x => x.Ma_Hoadon == MaHoadon).ToList();
            var tontaind = tontaihd.Where(x => x.Ma_Nguoidung == MaKhachhang).FirstOrDefault();

            if(tontaind == null)
            {
                await context.Database.ExecuteSqlRawAsync("EXEC XacNhanGiaoHang {0},{1}", MaKhachhang, MaHoadon);
                await context.SaveChangesAsync();
                return RedirectToAction("HoaDonDaGiao", "SanPham");
            }
            else
            {
                await context.Database.ExecuteSqlRawAsync("EXEC XacNhanGiaoHang_YC {0},{1}", MaKhachhang, MaHoadon);
                await context.SaveChangesAsync();
                return RedirectToAction("HoaDonDaGiao", "SanPham");
            }

            
        }
        public IActionResult HoaDonDaGiao(int page = 1)
		{
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            ViewBag.TB = da.Count();
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();
            var username = HttpContext.Session.GetString("tk");
			ViewBag.Username = username;
			page = page < 1 ? 1 : page;
			int pagesize = 4;
			var data = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList().ToPagedList(page, pagesize);
			return View(data);
		}

		public IActionResult ChiTietHoaDon_DaXuLy(string MaHoadon)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.TB = da.Count();

            ViewBag.YC = da.ToList();
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();
            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;
            //var data = context.ChiTietHoaDon_ListSPs.FromSqlRaw("ChiTietHoaDon_ChuaXuLy {0}", MaHoadon).ToList();
            //return View(data);
            var dataLinQ = from sp1 in context.Sanphams
                           join di in context.Discounts on sp1.MaGiamgia equals di.MaGiamgia into sp_di
                           from sp in sp_di.DefaultIfEmpty()
                           join asp in context.AnhSps on sp1.Ma_Sanpham equals asp.MaSanpham
                           join ct in context.ChitietHoadons on sp1.Ma_Sanpham equals ct.MaSanpham
                           where ct.MaHoadon == MaHoadon
                           select new
                           {
                               MaHoadon = ct.MaHoadon,
                               MaSanpham = sp1.Ma_Sanpham,
                               Ten_Sanpham = sp1.Ten_Sanpham,
                               Link1 = asp.Link1,
                               Link2 = asp.Link2,
                               Link3 = asp.Link3,
                               size = ct.size,
                               Soluong = ct.Soluong,
                               Giagoc = sp1.Giagoc,
                               MaGiamgia = sp.MaGiamgia,
                               Trangthai = ct.Trangthai,
                               Tilegiamgia = (sp.MaGiamgia == null) ? 0 : sp.Tilegiamgia
                           };

            List<ChiTietHoaDon_ListSP> data = new List<ChiTietHoaDon_ListSP>();
            foreach (var item in dataLinQ)
            {
                ChiTietHoaDon_ListSP sp = new ChiTietHoaDon_ListSP();
                sp.MaHoadon = item.MaHoadon;
                sp.MaSanpham = item.MaSanpham;
                sp.Ten_Sanpham = item.Ten_Sanpham;
                sp.Link1 = item.Link1;
                sp.Link2 = item.Link2;
                sp.Link3 = item.Link3;
                sp.size = item.size;
                sp.Soluong = item.Soluong;
                sp.Giagoc = item.Giagoc;
                sp.MaGiamgia = item.MaGiamgia;
                sp.Trangthai = item.Trangthai;
                sp.Tilegiamgia = item.Tilegiamgia;
                data.Add(sp);
            }

            var data1 = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            List<HienThiHoaDon> hd = new List<HienThiHoaDon>();
            hd = data1;

            var tt = hd.Where(x => x.MaHoadon == MaHoadon).FirstOrDefault();
            ViewBag.ThongTin = tt;

            return View(data);
        }

        public IActionResult ThongBao(int page = 1)
        {
            var da = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList();
            ViewBag.YC = da.ToList();
            ViewBag.TB = da.Count();
            
            var cxl = context.HienThiHoaDons.FromSqlRaw("HD_ChuaXuLy").ToList();
            ViewBag.CXL = cxl.Count();

            var dxl = context.HienThiHoaDons.FromSqlRaw("HD_DaXuLy").ToList();
            ViewBag.DXL = dxl.Count();

            var dg = context.HienThiHoaDons.FromSqlRaw("HD_DaGiao").ToList();
            ViewBag.DG = dg.Count();

            var username = HttpContext.Session.GetString("tk");
            ViewBag.Username = username;

            page = page < 1 ? 1 : page;
            int pagesize = 4;
            var data = context.HienThiYeuCaus.FromSqlRaw("YeuCauKH").ToList().ToPagedList(page, pagesize);

            return View(data);
        }
    }
}
 