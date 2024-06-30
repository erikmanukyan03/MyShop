using BLL.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
	public interface ICartService
	{
		void Add(CartVM model);
		void Update(CartVM model);
		List<CartVM> GetAll(bool isDeleted);
		List<CartVM> GetByCookie(string cookieId);
		void Delete(int Id);
		void DeleteAll(string cookieId);
		CartVM GetForEdit(int cartId);
		int ProdsCount(string cookieId);
		public List<CartVM> GetHistory(string cookieId);

    }
}
