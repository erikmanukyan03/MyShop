using Domain.Entities;
using Domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly MyShopDb _context;
        public OrderRepository(MyShopDb context)
        {
			_context = context;
        }
        public void Add(Order model)
		{
			_context.Orders.Add(model);
		}

		//public void Delete(int Id)
		//{
		//	var entity = _context.Orders.FirstOrDefault(c => c.Id == Id);
		//	entity.IsDeleted=true;
		//}
	
		//public void Update(Order model)
		//{
		//	var entity = _context.Orders.FirstOrDefault(c => c.Id == model.Id);
		//	entity.CustomerId=model.CustomerId;
		//	entity.OrderDate == model.OrderDate;
		//	entity.IsDeleted = model.IsDeleted;
		//}
	}
}
