using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Cart
	{
		public int Id { get; set; }
		public string CookieId { get; set; } = null!;
		public int Count { get; set; }
		public double? Price { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		public int ProductId { get; set; }
		public bool IsDeleted { get; set; }
	}
}
