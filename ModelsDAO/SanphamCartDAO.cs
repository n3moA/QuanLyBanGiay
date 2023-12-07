using QuanLyBanGiay_ADMIN.Data;
using QuanLyBanGiay_ADMIN.Models;
using QuanLyBanGiay_ADMIN.Models_User; 

namespace QuanLyBanGiay_ADMIN.ModelsDAO
{
    public class SanphamCartDAO
    {
        private readonly BanGiayContext _context;

        public SanphamCartDAO(BanGiayContext context)
        {
            _context = context;
        }

        public List<SanphamCart> GetAll()
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
                                 Tilegiamgia = discount.Tilegiamgia
                             };

            List<SanphamCart> dsht = new List<SanphamCart>();
            foreach (var item in danhsachsp)
            {
                SanphamCart spht = new SanphamCart();
                spht.Masanpham = item.Masanpham;
                spht.TenSanpham = item.TenSanpham;
                spht.Giagoc = item.Giagoc;
                spht.Anhsp = item.Anhsp;
                spht.Tilegiamgia = (int)item.Tilegiamgia*100;
                dsht.Add(spht);
            }
            return dsht;
        }

    }
}
