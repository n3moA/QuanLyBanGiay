using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User;
using QuanlyBG.Data;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace QuanLyBanGiay_ADMIN.Data;

public partial class BanGiayContext : DbContext
{
    public BanGiayContext()
    {
    }

    public BanGiayContext(DbContextOptions<BanGiayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSp> AnhSps { get; set; }
	public virtual DbSet<MotaSP> MotaSps { get; set; } = null!;
	public virtual DbSet<ChitietHoadon> ChitietHoadons { get; set; }

    public virtual DbSet<DanhgiaSp> DanhgiaSps { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Giohang> Giohangs { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<LoaiGiay> LoaiGiays { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<Nhasanxuat> Nhasanxuats { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }
    public virtual DbSet<SoLuong> SoLuongs { get; set; }
    public virtual DbSet<ThungRac> ThungRacs { get; set; }
    public virtual DbSet<Size> SanPhamSizes { get; set; }
    public virtual DbSet<SanPham_Size> SanPham_Sizes { get; set; }
    public virtual DbSet<HienThiYeuCau> HienThiYeuCaus { get; set; }
    public virtual DbSet<YeuCau> YeuCaus { get; set; }
    public virtual DbSet<ThongTinNguoiDung> ThongTinNguoiDungs { get; set; }
    public virtual DbSet<ListSanPham_Voucher> ListSanPham_Vouchers { get; set; }
    public virtual DbSet<SanPham_Applied> SanPham_Applieds { get; set; }
    public virtual DbSet<HienThiSanPham> HienThiSanPhams { get; set; }
    public virtual DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
    public virtual DbSet<Size_theoSanPham> Size_TheoSanPhams { get; set; }
    public virtual DbSet<HienThiHoaDon> HienThiHoaDons { get; set; }
    public virtual DbSet<ChiTietHoaDon_ListSP> ChiTietHoaDon_ListSPs { get; set; }
    public virtual DbSet<LichSuMuaHang> LichSuMuaHangs { get; set; }
	public virtual DbSet<SanphamHoadon> SanphamHoadons { get; set; } = null!;
	public IQueryable<SanPham_Size> Size_theoSanPham(string keyword)
    {
        SqlParameter sqlParameter = new SqlParameter("@MaSP", keyword);
        return this.SanPham_Sizes.FromSqlRaw("EXEC Size_theoSanPham @MaSP", sqlParameter); //trả về list
    }
    public IQueryable<SanPham_Size> Size_SoLuong(string MaSP, string Si)
    {
        SqlParameter sqlParameter = new SqlParameter("@MaSP", MaSP);
        SqlParameter sqlParametersi = new SqlParameter("@MaSize", Si);
        return this.SanPham_Sizes.FromSqlRaw("EXEC Tim_size @MaSP,@MaSize", sqlParameter, sqlParametersi); //trả về list
    }

    public IQueryable<HienThiSanPham> hienThiSanPhams(string Ten_Nhasanxuat)
    {
        SqlParameter sqlParameter = new SqlParameter("@nsx", Ten_Nhasanxuat);
        return this.HienThiSanPhams.FromSqlRaw("EXEC Loc_NhaSanXuat @nsx", sqlParameter);
    }

    public IQueryable<HienThiSanPham> TimTen_All(string Ten_Sanpham)
    {
        SqlParameter sqlParameter = new SqlParameter("@tensp", Ten_Sanpham);
        return this.HienThiSanPhams.FromSqlRaw("EXEC TimTen_All @tensp", sqlParameter);
    }
    public IQueryable<HienThiSanPham> TimTen_NSX(string Ten_Nhasanxuat, string Ten_Sanpham)
    {
        SqlParameter sqlParameter0 = new SqlParameter("@nsx", Ten_Nhasanxuat);
        SqlParameter sqlParameter = new SqlParameter("@tensp", Ten_Sanpham);
        return this.HienThiSanPhams.FromSqlRaw("EXEC TimTen_NSX @nsx, @tensp", sqlParameter0, sqlParameter);
    }

    public IQueryable<ThongTinNguoiDung> TimKiem_NguoiDung(string TenNguoidung)
    {
        SqlParameter sqlParameter = new SqlParameter("@ten", TenNguoidung);
        return this.ThongTinNguoiDungs.FromSqlRaw("EXEC TimKiem_NguoiDung @ten", sqlParameter);
    }
    public IQueryable<Size_theoSanPham> LocSize_theoSanPham(string Ma_Sanpham)
    {
        SqlParameter sqlParameter = new SqlParameter("@MaSP", Ma_Sanpham);
        return this.Size_TheoSanPhams.FromSqlRaw("EXEC Size_theoSanPham @MaSP", sqlParameter);
    }
    public IQueryable<Size> ThemSize_Moi(string Ma_Sanpham)
    {
        SqlParameter sqlParameter = new SqlParameter("@MaSP", Ma_Sanpham);
        return this.SanPhamSizes.FromSqlRaw("EXEC LaySize_Moi @MaSP", sqlParameter);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<SanphamHoadon>(entity => { entity.HasNoKey(); });

		modelBuilder.Entity<AnhSp>(entity =>
        {
            entity.HasKey(e => e.MaSanpham);

            entity.ToTable("AnhSP");

            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Link1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Link2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Link3)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

		modelBuilder.Entity<MotaSP>(entity =>
		{
			entity.HasNoKey();

			entity.ToTable("MotaSP");

            entity.Property(e => e.Mota)
                .IsUnicode(false);

            entity.Property(e => e.Thongso)
                .IsUnicode(false);

            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
        });

		modelBuilder.Entity<Size_theoSanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanpham);

            entity.ToTable("SizeSanPham");

            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.Link1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link1");
            entity.Property(e => e.Link2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link2");
            entity.Property(e => e.Link3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link3");
            entity.Property(e => e.Ma_Size)
               .HasMaxLength(3)
               .IsUnicode(false)
               .HasColumnName("Ma_Size"); ;
            entity.Property(e => e.SoLuong)
               .HasMaxLength(10)
               .IsUnicode(false)
               .HasColumnName("SoLuong");
        });

        modelBuilder.Entity<SoLuong>(entity =>
        {
            entity.HasKey(e => new { e.sodem});
           
        });

        modelBuilder.Entity<ChitietHoadon>(entity =>
        {
            entity.HasKey(e => new { e.MaHoadon, e.MaSanpham,e.size });
            entity.ToTable("Chitiet_Hoadon");

            entity.Property(e => e.MaHoadon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Hoadon");
            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.size)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("size");
            entity.HasOne(d => d.MaSanphamNavigation).WithMany(p => p.ChitietHoadons)
                .HasForeignKey(d => d.MaSanpham)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Chitiet_Hoadon_Sanpham");
        });

        modelBuilder.Entity<ChiTietHoaDon_ListSP>(entity =>
        {
            entity.HasKey(e => new { e.MaHoadon, e.MaSanpham, e.size });
        });

        modelBuilder.Entity<YeuCau>(entity =>
        {
            entity.HasKey(e => e.ID);
        });

        modelBuilder.Entity<HienThiYeuCau>(entity =>
        {
            entity.HasKey(e => e.ID);
            //entity.Property(e => e.Ten_Nguoidung)
            //    .HasMaxLength(50)
            //    .IsUnicode(false)
            //    .HasColumnName("Ten_Nguoidung");
            //entity.Property(e => e.Thong_Bao)
            //    .HasMaxLength(10)
            //    .IsUnicode(false)
            //    .HasColumnName("Thong_Bao");

        });

        modelBuilder.Entity<LichSuMuaHang>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.Ma_Nguoidung).HasColumnName("Ma_Nguoidung");
            entity.ToTable("LichSuMuaHang");
            //entity.Property(e => e.MaHoadon).HasColumnName("MaHoadon");
            //entity.Property(e => e.Ngaydathang).HasColumnName("Ngaydathang");
            //entity.Property(e => e.MaSanpham).HasColumnName("MaSanpham");
            //entity.Property(e => e.Ten_Sanpham).HasColumnName("Ten_Sanpham");
            //entity.Property(e => e.Link1).HasColumnName("Link1");
            //entity.Property(e => e.Link2).HasColumnName("Link2");
            //entity.Property(e => e.Link3).HasColumnName("Link3");
            //entity.Property(e => e.size).HasColumnName("size");
            //entity.Property(e => e.Soluong).HasColumnName("Soluong");
            //entity.Property(e => e.Giagoc).HasColumnName("Giagoc");
            //entity.Property(e => e.MaGiamgia).HasColumnName("MaGiamgia");
            //entity.Property(e => e.Thanhtien).HasColumnName("Thanhtien");

        });

        modelBuilder.Entity<HienThiHoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoadon);

        });
        modelBuilder.Entity<DanhgiaSp>(entity =>
        {
            entity.HasKey(e => e.MaDanhgia);

            entity.ToTable("DanhgiaSP");

            entity.Property(e => e.MaDanhgia)
                .HasColumnName("Ma_Danhgia");

            entity.Property(e => e.Danhgia).HasColumnType("ntext");
			entity.Property(e => e.Danhgia)
					.HasColumnType("nvarchar(MAX)")
					.HasColumnName("Danhgia");

			entity.Property(e => e.MaNguoidung)
					.HasMaxLength(10)
					.IsUnicode(false)
					.HasColumnName("Ma_Nguoidung");

			entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");

            entity.HasOne(d => d.MaNguoidungNavigation).WithMany(p => p.DanhgiaSps)
                .HasForeignKey(d => d.MaNguoidung)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DanhgiaSP_Nguoidung");

            entity.HasOne(d => d.MaSanphamNavigation).WithMany(p => p.DanhgiaSps)
                .HasForeignKey(d => d.MaSanpham)
                .HasConstraintName("FK_DanhgiaSP_Sanpham");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.MaGiamgia);

            entity.ToTable("Discount");

            entity.Property(e => e.MaGiamgia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Giamgia");
            entity.Property(e => e.Ngaygiamgia).HasColumnType("datetime");
            entity.Property(e => e.Ngayhethan).HasColumnType("datetime");
            entity.Property(e => e.Tilegiamgia).HasColumnType("float");
            entity.Property(e => e.MaDieukien)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Dieukien");
            entity.Property(e => e.DieuKien)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DieuKien");
        });

        modelBuilder.Entity<Giohang>(entity =>
        {
            entity.HasKey(e => e.MaGiohang);

            entity.ToTable("Giohang");

            entity.Property(e => e.MaGiohang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Giohang");
            entity.Property(e => e.MaKhachhang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Khachhang");
            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_sanpham");

            entity.HasOne(d => d.MaKhachhangNavigation).WithMany(p => p.Giohangs)
                .HasForeignKey(d => d.MaKhachhang)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Giohang_Nguoidung");

            entity.HasOne(d => d.MaSanphamNavigation).WithMany(p => p.Giohangs)
                .HasForeignKey(d => d.MaSanpham)
                .HasConstraintName("FK_Giohang_Sanpham");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.MaHoadon);

            entity.ToTable("Hoadon");

            entity.Property(e => e.MaHoadon)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Ma_Hoadon");
            entity.Property(e => e.MaKhachhang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Khachhang");
            entity.Property(e => e.Ngaydathang).HasColumnType("datetime");
            entity.Property(e => e.PhuongthucTt)
                .HasMaxLength(10)
                .HasColumnName("PhuongthucTT");

            
        });

        
        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.MaNguoidung).HasName("PK_Nhanvien");

            entity.ToTable("Nguoidung");

            entity.Property(e => e.MaNguoidung)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Nguoidung");
            entity.Property(e => e.DiachiNv)
                .HasMaxLength(40)
                .HasColumnName("Diachi_nv");
            entity.Property(e => e.Gioitinh).HasMaxLength(10);
            entity.Property(e => e.MaChucvu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Chucvu");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SodienthoaiNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Sodienthoai_nv");
            entity.Property(e => e.TenNguoidung)
                .HasMaxLength(40)
                .HasColumnName("Ten_Nguoidung");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Nhasanxuat>(entity =>
        {
            entity.HasKey(e => e.MaNhasanxuat);

            entity.ToTable("Nhasanxuat");

            entity.Property(e => e.MaNhasanxuat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhasanxuat");
            entity.Property(e => e.DiachiNsx)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Diachi_nsx");
            entity.Property(e => e.Ghichu).HasColumnType("text");
            entity.Property(e => e.SodienthoaiNsx)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Sodienthoai_nsx");
            entity.Property(e => e.TenNhasanxuat)
                .HasMaxLength(50)
                .HasColumnName("Ten_Nhasanxuat");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Ma_Sanpham);

            entity.ToTable("Sanpham");

            entity.Property(e => e.Ma_Sanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.MaNhasanxuat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhasanxuat"); 
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Loai"); 
            entity.Property(e => e.Mausac)
                .HasMaxLength(50)
                .HasColumnName("Mausac"); 
            entity.Property(e => e.MaGiamgia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Giamgia");
            
            entity.Property(e => e.Giagoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Giagoc");


            entity.HasOne(d => d.MaSanphamNavigation).WithOne(p => p.Sanpham)
                .HasForeignKey<Sanpham>(d => d.Ma_Sanpham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sanpham_AnhSP");

        });

        modelBuilder.Entity<ThungRac>(entity =>
        {
            entity.HasKey(e => e.Ma_Sanpham);

            entity.ToTable("ThungRac");

            entity.Property(e => e.Ma_Sanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.MaNhasanxuat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhasanxuat");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Loai");
            entity.Property(e => e.Mausac)
                .HasMaxLength(50)
                .HasColumnName("Mausac");
            entity.Property(e => e.MaGiamgia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Giamgia");

            entity.Property(e => e.Giagoc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Giagoc");

        });

 

        modelBuilder.Entity<ThongTinNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoidung).HasName("PK_Nhanvien1");

            entity.ToTable("ThongTin");

            entity.Property(e => e.MaNguoidung)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Nguoidung");
            entity.Property(e => e.DiachiNv)
                .HasMaxLength(40)
                .HasColumnName("Diachi_nv");
            entity.Property(e => e.Gioitinh).HasMaxLength(10);
            entity.Property(e => e.Chucvu)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Chucvu");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.SodienthoaiNv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Sodienthoai_nv");
            entity.Property(e => e.TenNguoidung)
                .HasMaxLength(40)
                .HasColumnName("Ten_Nguoidung");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaChucvu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Chucvu");
        });

        modelBuilder.Entity<HienThiSanPham>(entity =>
        {
            entity.HasKey(e => e.Ma_Sanpham).HasName("PK_SanPham1");

            entity.ToTable("HienThi");

            entity.Property(e => e.Ma_Sanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.Mausac).HasMaxLength(10);
            entity.Property(e => e.Ten_Nhasanxuat)
                .HasMaxLength(50)
                .HasColumnName("Ten_Nhasanxuat");
        entity.Property(e => e.Ten_loai)
                .HasMaxLength(50)
                .HasColumnName("Ten_loai");
            entity.Property(e => e.Link1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link1");
            entity.Property(e => e.Link2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link2"); 
            entity.Property(e => e.Link3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link3");
            entity.Property(e => e.All_Size)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("All_Size");
            entity.Property(e => e.So_Luong)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("So_Luong");
            entity.Property(e => e.Giagoc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Giagoc");
        });

        modelBuilder.Entity<ListSanPham_Voucher>(entity =>
        {
            entity.HasKey(e => e.Ma_Sanpham).HasName("PK_SanPham7");

            entity.ToTable("LstSP_V");

            entity.Property(e => e.Ma_Sanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.Mausac)
            .HasMaxLength(10)
            .HasColumnName("Mausac");
            entity.Property(e => e.Ten_Nhasanxuat)
                .HasMaxLength(50)
                .HasColumnName("Ten_Nhasanxuat");
            entity.Property(e => e.Ten_loai)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_loai");
            entity.Property(e => e.Link1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link1");
            entity.Property(e => e.Link2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link2");
            entity.Property(e => e.Link3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link3");
            entity.Property(e => e.Giagoc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Giagoc");
        });

        modelBuilder.Entity<SanPham_Applied>(entity =>
        {
            entity.HasKey(e => e.Ma_Sanpham).HasName("PK_SanPham8");

            entity.ToTable("SP_V");

            entity.Property(e => e.Ma_Sanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.Mausac)
            .HasMaxLength(10)
            .HasColumnName("Mausac");
            entity.Property(e => e.Ten_Nhasanxuat)
                .HasMaxLength(50)
                .HasColumnName("Ten_Nhasanxuat");
            entity.Property(e => e.Ten_loai)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_loai");
            entity.Property(e => e.Link1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link1");
            entity.Property(e => e.Link2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link2");
            entity.Property(e => e.Link3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link3");
            entity.Property(e => e.Giagoc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Giagoc");
            entity.Property(e => e.GiaApDung)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GiaApDung");
            entity.Property(e => e.Ma_Giamgia)
               .HasMaxLength(20)
               .IsUnicode(false)
               .HasColumnName("Ma_Giamgia");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucvu);

            entity.ToTable("ChucVu");

            entity.Property(e => e.MaChucvu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Chucvu");
            entity.Property(e => e.TenChucVu)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("TenChucVu");
            
        });
        modelBuilder.Entity<LoaiGiay>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiGiay");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Loai");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Ten_loai");
        });



        modelBuilder.Entity<SanPham_Size>(entity =>
        {
            entity.HasKey(e => new { e.MaSanpham, e.Ma_Size });

            entity.ToTable("SanPham_Size");

            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_SanPham");

            entity.Property(e => e.Ma_Size)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Ma_Size");
            entity.Property(e => e.SoLuong)
               .HasMaxLength(3)
               .IsUnicode(false)
               .HasColumnName("SoLuong");

            entity.HasOne(d => d.MaSanPhamNavigation)
                .WithMany(p => p.SanPham_Sizes)
                .HasForeignKey(d => d.MaSanpham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_Size_Sanpham");

            entity.HasOne(d => d.MaSizeNavigation)
                .WithMany(p => p.SanPhamSizes)
                .HasForeignKey(d => d.Ma_Size)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_Size_Size");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Ma_Size);

            entity.ToTable("Size");

            entity.Property(e => e.Ma_Size)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Ma_Size");

            entity.Property(e => e._Size)
            .HasMaxLength(3)
            .IsUnicode(false)
            .HasColumnName("_Size");
        });

        modelBuilder.Entity<ChiTietSanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanpham).HasName("PK_SanPham3");

            entity.ToTable("ChiTiet");

            entity.Property(e => e.MaSanpham)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Sanpham");
            entity.Property(e => e.Ten_Sanpham)
                .HasMaxLength(50)
                .HasColumnName("Ten_Sanpham");
            entity.Property(e => e.Mausac).HasMaxLength(10);
            entity.Property(e => e.MaNhasanxuat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Nhasanxuat");
            entity.Property(e => e.Ten_Nhasanxuat)
                .HasMaxLength(50)
                .HasColumnName("Ten_Nhasanxuat");
            entity.Property(e => e.MaLoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Ma_Loai");
            entity.Property(e => e.Ten_loai)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_loai");
            entity.Property(e => e.Link1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link1");
            entity.Property(e => e.Link2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link2");
            entity.Property(e => e.Link3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Link3");
           
            entity.Property(e => e.Giagoc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Giagoc");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
