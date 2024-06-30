using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	 public class CategoryRepository : ICategoryRepository
	{
		private readonly MyShopDb _context;
		public CategoryRepository(MyShopDb context)
		{
			_context = context;
		}
		public void Add(Category model)
		{
			_context.Categories.Add(model);
		}

		public void Delete(int Id)
		{
			var entity = _context.Categories.Include(c=>c.Products).FirstOrDefault(c => c.Id == Id);
			if (entity != null)
			{
				foreach (var item in entity.Products)
				{
					item.IsDeleted = true;
				}
			}
			entity.IsDeleted = true;
		}

		public void Update(Category model)
		{
			var entity = _context.Categories.FirstOrDefault(c => c.Id == model.Id);
			entity.Name=model.Name;
			entity.Description=model.Description;
			entity.IsDeleted = model.IsDeleted;
			//entity.MetaDescription = model.MetaDescription;
			//entity.PageTitle = model.PageTitle;
			//entity.Slug = model.Slug;
		}
	}
}
