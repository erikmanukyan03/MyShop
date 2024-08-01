using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class ProductForSearch
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Slug { get; set; }
		public string? ImageFile { get; set; }
		public ProdColor ProdColor { get; set; }
		public List<Atribute_ValueVM>? PAVVMs { get; set; }
		public string ShortDescription { get; set; }
		public int Memory { get; set; }
	}
}
