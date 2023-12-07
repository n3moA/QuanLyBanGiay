using System.Security.Permissions;

namespace QuanLyBanGiay_ADMIN.Models_User.ViewModels
{
	public class PagingInfo
	{ 
		public int TotalItems { get; set; }

		public int ItemPerPage { get; set; }

		public int CurrentPage { get; set; }

		public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemPerPage);
	}
}
