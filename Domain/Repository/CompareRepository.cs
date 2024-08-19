using Domain.Entities;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public class CompareRepository  : ICompareRepository
	{
		private readonly MyShopDb _context;
		public CompareRepository(MyShopDb context)
		{
			_context = context;
		}
		public void Add(Compare model)
		{
			_context.Compares.Add(model);
		}

		public void Delete(int Id)
		{
			var entity = _context.Compares.Where(c => c.ProductId == Id).ExecuteDelete();
		}
		public void DeleteAll(string cookieId,int categoryId)
		{
			var list = _context.Compares.Where(c => c.CookieId == cookieId && c.CategoryId==categoryId).ToList();
		}
	}
}
