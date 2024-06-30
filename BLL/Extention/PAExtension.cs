using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extention
{
    public static class PAExtension
    {
        public static ProductAttribute GetById(this DbSet<ProductAttribute> db,int Id) 
        {
                var a=db.Include(p => p.AttributeValues).FirstOrDefault(p => p.Id == Id);
            return a;
        }
        public static List<ProductAttribute> GetByProdId(this DbSet<ProductAttribute> db,int prodId)
        {
            //.Where(p => p.ProductId == prodId).ToList()
            return db.Include(p=>p.AttributeValues).ToList();
        }
        public static List<ProductAttribute> GetAll(this DbSet<ProductAttribute> db)
        {
            return db.Include(p => p.AttributeValues).ToList();
        }

    }
}
