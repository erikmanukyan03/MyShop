using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repository.Interfaces
{
	public interface ICartRepository
	{
		public void Add(Cart model);
		public void Delete(int Id);
		public void DeleteAll(string cookieId);
		public void Update(Cart model);
	}
}
