using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public string CookieId { get; set; }
		[ForeignKey("CustomerId")]
		public Customer Customer { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public bool IsDeleted { get; set; }
	}
}