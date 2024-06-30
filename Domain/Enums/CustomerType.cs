﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
	public enum CustomerType
	{
		[Display(Name = "Покупатель")]
		Buyer,
        [Display(Name = "Обратный звонок")]
		СallBack,
	}
}
