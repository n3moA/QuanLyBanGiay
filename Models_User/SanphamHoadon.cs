using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace QuanLyBanGiay_ADMIN.Models_User
{
    public class SanphamHoadon
    {
        public string? Ma_Sanpham { get; set; }
		public string? Ten_Sanpham { get; set; }
        public int Size { get; set; }
        public int Soluong { get; set; }
        public int Giaban { get; set; }
        public string? Mausac { get; set; } 
        public string? Link1 { get; set; }

    }
}
