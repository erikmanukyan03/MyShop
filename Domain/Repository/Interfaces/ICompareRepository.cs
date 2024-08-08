using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interfaces
{
	public interface ICompareRepository
	{
		public void Add(Compare model);

		public void Delete(int Id);
		public void DeleteAll(string cookieId, int categoryId);
	}
}
