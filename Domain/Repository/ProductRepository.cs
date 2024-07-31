using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly MyShopDb _context;
		public ProductRepository(MyShopDb context)
		{
			_context = context;
		}
		public void Add(Product model)
		{
			_context.Products.Add(model);
		}

		public void Delete(int Id)
		{
			var entity = _context.Products.FirstOrDefault(c => c.Id == Id);
			entity.IsDeleted = true;
		}

		public void Update(Product model)
		{
			var entity = _context.Products.FirstOrDefault(c => c.Id == model.Id);
			entity.Title = model.Title;
			entity.Description = model.Description;
			entity.ShortDescription = model.ShortDescription;
			entity.IsHot = model.IsHot;
			entity.Discount = model.Discount;
			entity.Memory= model.Memory;
			entity.Price=model.Price;
			if (model.MainImage != null)
			{
				entity.MainImage = model.MainImage;
			}
			entity.IsDeleted = model.IsDeleted;
			entity.ProdColor = model.ProdColor;
			entity.CategoryId = model.CategoryId;
			entity.Slug = model.Slug;
			entity.PageTitle = model.PageTitle;
			entity.MetaDescription= model.MetaDescription;
			//entity.PAV= model.PAV;
		}
		public void DeleteImage(int ImageId)
		{
			_context.ProductImages.Where(pi => pi.Id == ImageId).ExecuteDelete();
		}
	}
}

