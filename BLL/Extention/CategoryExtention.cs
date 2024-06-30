using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extention
{
    public static class CategoryExtention
    {
        public static Category GetById(this DbSet<Category> db,int Id)
        {
            var a= db.Include(c=>c.Products).FirstOrDefault(p=>p.Id == Id);
            return a;
        }
        public async static Task<List<Category>> GetAll(this DbSet<Category> db, bool IsDeleted)
        {
            return await db.Include(c=>c.Products).Where(c=>c.IsDeleted==IsDeleted).ToListAsync();
        }
        public async static Task<List<Category>> GetForProducts(this DbSet<Category> db)
        {
            return await db.Where(c=>c.IsDeleted==false).Select(c=>new Category
            {
                Id=c.Id,
                Name=c.Name,
                Products = c.Products,
                //MetaDescription = c.MetaDescription,
                //Slug=c.Slug,
                //PageTitle=c.PageTitle,
            }).ToListAsync();
        }
        public async static Task<List<Category>> GetCategoryNames(this DbSet<Category> db)
        {
            return await db.Where(c => c.IsDeleted == false).Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }

    }
}
