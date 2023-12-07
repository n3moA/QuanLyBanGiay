namespace QuanLyBanGiay_ADMIN.Models
{
    public class Size
    {

        public string Ma_Size { get; set; }
        public int? _Size { get; set; }

        public virtual ICollection<SanPham_Size> SanPhamSizes { get; set; } = new List<SanPham_Size>();
    }
}
