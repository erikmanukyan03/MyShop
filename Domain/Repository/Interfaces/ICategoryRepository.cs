using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface ICategoryRepository
	{
		public void Add(Category model);
		public void Delete(int Id);
		public void Update(Category model);
	}
}
