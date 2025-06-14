﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ProductImage
	{
		public int Id { get; set; }
		public string ImageFile { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		public int ProductId { get; set; }
	}
}
