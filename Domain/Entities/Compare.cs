﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Compare
	{
		public int Id { get; set; }
		public string CookieId { get; set; } = null!;
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
	}
}
