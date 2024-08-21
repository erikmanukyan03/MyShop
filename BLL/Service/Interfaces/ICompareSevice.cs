using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
	public interface ICompareSevice
	{
		void Add(int prodId,string cookieId);
		void Delete(int Id);
		void DeleteAll(string cookieId, int categoryId);
        Dictionary<string, List<string>> GetAll(string cookieId);
		int GetCount(string cookieId);

	}
}
