using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class ProductCompareVM
	{
		public int Id { get; set; }
		public string Title { get; set; }
		//public ProdColor ProdColor { get; set; }
		public List<Atribute_ValueVM>? PAVVMs { get; set; }
		public int Memory { get; set; }
		public string Slug { get; set; }
		public string? ImageFile { get; set; }
		public int Price { get; set; }
		public int? Discount = 0;
		public int CategoryId { get; set; }
		public bool IsHot { get; set; }
		public double FinalPrice { get; set; }
	}
}
