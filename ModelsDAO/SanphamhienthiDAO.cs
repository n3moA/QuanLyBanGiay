using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User;

namespace QuanLyBanGiay_ADMIN.ModelsDAO
{
	public class SanphamhienthiDAO
	{
		private readonly BanGiayContext _context;

		public SanphamhienthiDAO(BanGiayContext context)
		{
			_context = context;
		}

		public List<SanphamHienthi> GetAll()
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			foreach (var item in danhsachsp)
			{
				SanphamHienthi spht = new SanphamHienthi();
				spht.Masanpham = item.Masanpham;
				spht.TenSanpham = item.TenSanpham;
				spht.Giagoc = item.Giagoc;
				spht.Anhsp = item.Anhsp;
				spht.Anhhover = item.Anhhover;
				spht.MaGiamgia = item.Magiamgia;
				spht.LoaiGiay = item.LoaiGiay;
				spht.Mausac = item.Mausac;
				spht.Hangsanxuat = item.Hangsanxuat;
				spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
				dsht.Add(spht);
			}
			return dsht;
		}

		public List<SanphamHienthi> Laysanphammoi()
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 orderby sanpham.MaNhasanxuat descending
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			int i = 0;
			foreach (var item in danhsachsp)
			{
				if (i < 8)
				{
					SanphamHienthi spht = new SanphamHienthi();
					spht.Masanpham = item.Masanpham;
					spht.TenSanpham = item.TenSanpham;
					spht.Giagoc = item.Giagoc;
					spht.Anhsp = item.Anhsp;
					spht.Anhhover = item.Anhhover;
					spht.MaGiamgia = item.Magiamgia;
					spht.LoaiGiay = item.LoaiGiay;
					spht.Mausac = item.Mausac;
					spht.Hangsanxuat = item.Hangsanxuat;
					spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
					dsht.Add(spht);
				}
				i++;
			}
			return dsht;
		}

		public List<SanphamHienthi> Laysanseco()
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 orderby sanpham.Giagoc
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			int i = 0;
			foreach (var item in danhsachsp)
			{
				if (i < 8)
				{
					SanphamHienthi spht = new SanphamHienthi();
					spht.Masanpham = item.Masanpham;
					spht.TenSanpham = item.TenSanpham;
					spht.Giagoc = item.Giagoc;
					spht.Anhsp = item.Anhsp;
					spht.Anhhover = item.Anhhover;
					spht.MaGiamgia = item.Magiamgia;
					spht.LoaiGiay = item.LoaiGiay;
					spht.Mausac = item.Mausac;
					spht.Hangsanxuat = item.Hangsanxuat;
					spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
					dsht.Add(spht);
				}
				i++;
			}
			return dsht;
		}

		public List<SanphamHienthi> Laysanphamtheo_HSX(string hangsx)
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 where sanpham.MaNhasanxuat == hangsx
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			foreach (var item in danhsachsp)
			{
				SanphamHienthi spht = new SanphamHienthi();
				spht.Masanpham = item.Masanpham;
				spht.TenSanpham = item.TenSanpham;
				spht.Giagoc = item.Giagoc;
				spht.Anhsp = item.Anhsp;
				spht.Anhhover = item.Anhhover;
				spht.MaGiamgia = item.Magiamgia;
				spht.LoaiGiay = item.LoaiGiay;
				spht.Mausac = item.Mausac;
				spht.Hangsanxuat = item.Hangsanxuat;
				spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
				dsht.Add(spht);
			}
			return dsht;
		}

		public List<SanphamHienthi> Laysanphamtheo_Mau(string mausac)
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 where sanpham.Mausac == mausac
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			foreach (var item in danhsachsp)
			{
				SanphamHienthi spht = new SanphamHienthi();
				spht.Masanpham = item.Masanpham;
				spht.TenSanpham = item.TenSanpham;
				spht.Giagoc = item.Giagoc;
				spht.Anhsp = item.Anhsp;
				spht.Anhhover = item.Anhhover;
				spht.MaGiamgia = item.Magiamgia;
				spht.LoaiGiay = item.LoaiGiay;
				spht.Mausac = item.Mausac;
				spht.Hangsanxuat = item.Hangsanxuat;
				spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
				dsht.Add(spht);
			}
			return dsht;
		}

		public List<SanphamHienthi> Laysanphamtheo_Loaigiay(string loaigiay)
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 where sanpham.MaLoai == loaigiay
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			foreach (var item in danhsachsp)
			{
				SanphamHienthi spht = new SanphamHienthi();
				spht.Masanpham = item.Masanpham;
				spht.TenSanpham = item.TenSanpham;
				spht.Giagoc = item.Giagoc;
				spht.Anhsp = item.Anhsp;
				spht.Anhhover = item.Anhhover;
				spht.MaGiamgia = item.Magiamgia;
				spht.LoaiGiay = item.LoaiGiay;
				spht.Mausac = item.Mausac;
				spht.Hangsanxuat = item.Hangsanxuat;
				spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
				dsht.Add(spht);
			}
			return dsht;
		}

		public List<SanphamHienthi> Laysanphamtheo_Gia(int min, int max)
		{
			List<Sanpham> sanphams = _context.Sanphams.ToList();
			List<AnhSp> anhSp = _context.AnhSps.ToList();
			List<Discount> discounts = _context.Discounts.ToList();
			var danhsachsp = from sanpham in sanphams
							 join anh in anhSp on sanpham.Ma_Sanpham equals anh.MaSanpham
							 join discount in discounts on sanpham.MaGiamgia equals discount.MaGiamgia
							 where sanpham.Giagoc >= min && sanpham.Giagoc <= max
							 select new
							 {
								 Masanpham = sanpham.Ma_Sanpham,
								 TenSanpham = sanpham.Ten_Sanpham,
								 Giagoc = sanpham.Giagoc,
								 Anhsp = anh.Link1,
								 Anhhover = anh.Link2,
								 Magiamgia = sanpham.MaGiamgia,
								 LoaiGiay = sanpham.MaLoai,
								 Mausac = sanpham.Mausac,
								 Hangsanxuat = sanpham.MaNhasanxuat,
								 Tilegiamgia = discount.Tilegiamgia
							 };

			List<SanphamHienthi> dsht = new List<SanphamHienthi>();
			foreach (var item in danhsachsp)
			{
				SanphamHienthi spht = new SanphamHienthi();
				spht.Masanpham = item.Masanpham;
				spht.TenSanpham = item.TenSanpham;
				spht.Giagoc = item.Giagoc;
				spht.Anhsp = item.Anhsp;
				spht.Anhhover = item.Anhhover;
				spht.MaGiamgia = item.Magiamgia;
				spht.LoaiGiay = item.LoaiGiay;
				spht.Mausac = item.Mausac;
				spht.Hangsanxuat = item.Hangsanxuat;
				spht.Tilegiamgia = (int)((double)item.Tilegiamgia * 100);
				dsht.Add(spht);
			}
			return dsht;
		}
	}
}


