using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class CompareVM
	{
		public int Id { get; set; }
		public string CookieId { get; set; } = null!;
		public ProductCompareVM Product { get; set; }
		public int CategoryId { get; set; }
	}
}
