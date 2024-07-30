using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class CartVM
	{
		public int Id { get; set; }
		public string CookieId { get; set; } = null!;
		public int Count { get; set; }
		public double? Price { get; set; }
		public ShortProductVM Product { get; set; }
		public int ProductId { get; set; }
		public bool IsDeleted { get; set; }
	}
}
