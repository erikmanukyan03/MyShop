﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
	public class Customer
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public CustomerType RoleType { get; set; }
		public string? Message { get; set; }
		public bool IsDeleted { get; set; }
	}
}
