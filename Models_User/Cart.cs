using Microsoft.AspNetCore.Mvc;
using QuanLyBanGiay_ADMIN.Models;

namespace QuanLyBanGiay_ADMIN.Models_User
{
	public class Cart
	{
		public List<CartLine> Lines { get; set; } = new List<CartLine>();

		public void AddItem(SanphamCart product, int quantity)
		{
			CartLine? line = Lines
			.Where(p => p.Product.Masanpham == product.Masanpham && p.Product.size == product.size)
			.FirstOrDefault();
			if (line == null)
			{
				Lines.Add(new CartLine
				{
					Product = product,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public void UpdateLine(SanphamCart product, int quantity)
		{
			CartLine? line = Lines
			.Where(p => p.Product.Masanpham == product.Masanpham && p.Product.size == product.size)
			.FirstOrDefault();
			if (line != null)
			{
				line.Quantity = quantity;
			}
		}

		public void RemoveLine(SanphamCart product) => Lines.RemoveAll(l => l.Product.Masanpham == product.Masanpham && l.Product.size == product.size);

		public decimal ComputeTotalValue() => (decimal)Lines.Sum(e => ((((decimal)1 - (decimal)e.Product.Tilegiamgia / 100) * (decimal)e.Product.Giagoc) * (decimal)e.Quantity));

		public int ComputeTotalProducts() => (int)Lines.Sum(e => e.Quantity);

		public void Clear() => Lines.Clear();
	}
	public class CartLine
	{
		public int CartLineID { get; set; }
		public SanphamCart Product { get; set; } = new();
		public int Quantity { get; set; }
	}
}
