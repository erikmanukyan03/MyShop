using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extention
{
    public static class OrderExtention
    {
        public static Order GetById(this DbSet<Order> db, int Id)
        {
            return db.Include(o => o.Customer).FirstOrDefault(o => o.Id == Id);
        }
        public static List<Order> GetAll(this DbSet<Order> db, bool isDeleted)
        {
            return db.Where(o=>o.IsDeleted == isDeleted).Include(o => o.Customer).ToList();
        }
        public static List<Order> GetByCookie(this DbSet<Order> db, string cookieId)
        {
            return db.Where(o => o.CookieId == cookieId).Include(o => o.Customer).ToList();
        }
        
    }
}
