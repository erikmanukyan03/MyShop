using Domain.Entities;
using Domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public class CartRepository : ICartRepository
	{
		private readonly MyShopDb _context;
        public CartRepository(MyShopDb context)
        {
			_context = context;
        }
        public void Add(Cart model)
		{
			_context.Carts.Add(model);
		}

		public void Delete(int Id)
		{
			var entity = _context.Carts.FirstOrDefault(c => c.Id == Id);
			entity.IsDeleted=true;
		}
        public void DeleteAll(string cookieId)
        {
			var list = _context.Carts.Where(c => c.CookieId == cookieId).ToList();
            foreach(var entity in list)
			{
                entity.IsDeleted = true;
            }
        }

        public void Update(Cart model)
		{
			var entity = _context.Carts.FirstOrDefault(c => c.Id == model.Id);
			entity.Count = model.Count == null ? entity.Count : model.Count;
			entity.Price = model.Price;
			entity.IsDeleted = model.IsDeleted;
		}
	}
}
