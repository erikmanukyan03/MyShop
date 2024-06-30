using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repository
{
	public interface IOrderRepository
	{
		public void Add(Order model);
		//public void Delete(int Id);
		//public void Update(Cart model);
	}
}
