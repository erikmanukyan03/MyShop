using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels;
using BLL.ViewModels.Order;

namespace BLL.Service
{
	public interface IOrderService
	{
		EmailOrder GetEmailOrder(int Id);
        int Add(OrderVM model);
		List<OrderVM> GetAll(bool isDeleted);
		List<OrderVM> GetByCookie(string cookieId);
		OrderVM GetById(int Id);
	}
}
