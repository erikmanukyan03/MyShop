using BLL.ViewModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extention
{
    public static class CartExtention
    {
        public static List<Cart> GetAll(this DbSet<Cart> db,bool isDeleted)
        {
            return db.Where(c=>c.IsDeleted==isDeleted).Include(c=>c.Product).ToList();
        }
        public static List<Cart> GetByCookie(this DbSet<Cart> db, string cookieId)
        {
            return db.Where(c => c.CookieId == cookieId && c.IsDeleted==false).Include(c => c.Product).ToList();
        }
        public static List<Cart> GetHistory(this DbSet<Cart> db, string cookieId)
        {
            return db.Where(c => c.CookieId == cookieId && c.IsDeleted == true).Include(c => c.Product).ToList();
        }
        public static Cart GetForEdit(this DbSet<Cart> db,int cartId)
        {
            return db.FirstOrDefault(c => c.Id == cartId);
        }
        public static int ProdsCount(this DbSet<Cart> db, string cookieId)
        {
            return db.Where(s => s.CookieId == cookieId&& s.IsDeleted==false).Sum(c => c.Count);
        }
    }
}
