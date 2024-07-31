using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IProductRepository
	{
		public void Add(Product model);
		public void Delete(int Id);
		public void DeleteImage(int imageId);
		public void Update(Product model);
	}
}
