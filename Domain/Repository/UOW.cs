using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public class UOW : IUOW
	{
		private readonly MyShopDb _context;
        public UOW(MyShopDb context)
        {
			_context = context;
        }
        public void Save()
		{
			_context.SaveChanges();
		}
	}
}
