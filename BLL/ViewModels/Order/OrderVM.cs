using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
	public class OrderVM
	{
		public int Id { get; set; }
		public string CookieId { get; set; } = null!;
        public CustomerVM Customer { get; set; }
		public DateTime OrderDate { get; set; }
		public bool IsDeleted { get; set; }
	}
}

