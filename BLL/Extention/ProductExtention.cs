using BLL.ViewModels;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extention
{
    public static class ProductExtention
    {
        public static Product GetById(this DbSet<Product> db, int Id)
        {
            return db.Include(p => p.Category).Include(P => P.PAVs).ThenInclude(p => p.ProductAttribute).Include(p=>p.Images).FirstOrDefault(p => p.Id == Id);
        }
		public static List<Product> GetByIds(this DbSet<Product> db, List<int> Ids)
		{
            return db.Include(p => p.Category).Include(P => P.PAVs).ThenInclude(p => p.ProductAttribute).Include(p => p.Images).Where(p => Ids.Contains(p.Id)).ToList();
		}
		public static Product GetBySlug(this DbSet<Product> db, string slug)
        {
            return db.Include(p => p.Category).Include(P => P.PAVs).ThenInclude(p => p.ProductAttribute).Include(p => p.Images).FirstOrDefault(p => p.Slug == slug);
        }

        public async static Task<List<Product>> GetAll(this DbSet<Product> db, int? categoryId, bool IsDeleted)
        { 
            var list = await db.Include(p=>p.PAVs).ThenInclude(pa=>pa.ProductAttribute).Include(c => c.Category).Where(c =>c.IsDeleted == IsDeleted && (c.CategoryId==categoryId || categoryId==null)).Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
                MainImage=p.MainImage,
                Discount = p.Discount,
                Memory=p.Memory,
                ProdColor=p.ProdColor,
                MetaDescription = p.MetaDescription,
                Slug = p.Slug,
                PageTitle = p.PageTitle,
                PAVs =p.PAVs,
                CategoryId = p.CategoryId,
            }).ToListAsync();
            return list;
        }
		public static List<Product> GetAllForAdmin(this DbSet<Product> db, int? categoryId, bool IsDeleted)
		{
			var list = db.Include(p => p.PAVs).ThenInclude(pa => pa.ProductAttribute).Include(c => c.Category).Where(c => c.IsDeleted == IsDeleted && (c.CategoryId == categoryId || categoryId == null)).Select(p => new Product
			{
				Id = p.Id,
				Title = p.Title,
				ShortDescription = p.ShortDescription,
				Price = p.Price,
				MainImage = p.MainImage,
				Discount = p.Discount,
				Memory = p.Memory,
				ProdColor = p.ProdColor,
				MetaDescription = p.MetaDescription,
				Slug = p.Slug,
				PageTitle = p.PageTitle,
				PAVs = p.PAVs,
				CategoryId = p.CategoryId,
			}).ToList();
			return list;
		}
		public static async Task<Tuple<int?,int?>> MinMaxPrice(this DbSet<Product> db,int categoryId)
        {
            ////Chem jogm
            var min= await db.Include(p=>p.Category).Where(p=>p.CategoryId==categoryId).OrderBy(p => p.Discount != 0 ? p.Price - p.Price * p.Discount / 100 : p.Price).Select(p=> p.Discount != 0 ? p.Price - p.Price * p.Discount / 100 : p.Price).FirstOrDefaultAsync();
            var max=await db.Include(p=>p.Category).Where(p=>p.CategoryId==categoryId).OrderByDescending(p => p.Discount != 0 ? p.Price - p.Price * p.Discount / 100 : p.Price).Select(p => p.Discount != 0 ? p.Price - p.Price * p.Discount / 100 : p.Price).FirstOrDefaultAsync();
            return Tuple.Create(min,max);
        }
        public static async Task<List<ProductAttributeValue>> GetByCategory(this DbSet<Product> db, int Id)
        {
            var ps= await db.Include(p => p.Category).Include(p => p.PAVs).ThenInclude(p => p.ProductAttribute).Where(p => p.CategoryId == Id).SelectMany(p => p.PAVs).ToListAsync();
            return ps;
        }
        public async static Task<List<Product>> Filter(this DbSet<Product> db,List<int> Ids)
        {
            var list = await db.Include(c => c.Category).Include(p => p.PAVs).ThenInclude(p => p.ProductAttribute).Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
                MainImage = p.MainImage,
                PAVs=p.PAVs,
                Discount = p.Discount,
                CategoryId = p.CategoryId,
                Memory = p.Memory,
                ProdColor = p.ProdColor,
            }).ToListAsync();
            return list;
        }
        public async static Task<List<Product>> GetHots(this DbSet<Product> db)
        {
            var list = await db.Include(c => c.Category).Include(p=>p.PAVs).ThenInclude(p=>p.ProductAttribute).Where(c =>c.IsDeleted==false).Select(p => new Product
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Price = p.Price,
                MainImage = p.MainImage,
                Discount = p.Discount,
                Slug=p.Slug,
                PAVs=p.PAVs,
                CategoryId = p.CategoryId,
                Memory = p.Memory,
                ProdColor = p.ProdColor,
            }).ToListAsync();


            return list;
        }
    }
}
