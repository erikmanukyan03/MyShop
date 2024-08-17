using BLL.ViewModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class CompareExtention
    {
        public static List<int> GetAll(this DbSet<Compare> db,string cookieId)
        {
            return db.Where(c=>c.CookieId==cookieId).Select(p=>p.ProductId).ToList();
            
        }
        public static Compare GetByCookie(this DbSet<Compare> db, string cookieId,int prodId)
        {
            return db.FirstOrDefault(c => c.CookieId == cookieId && prodId==prodId);
        }
        public static bool IsCompared(this DbSet<Compare> db, string cookieId, int prodId)
        {
            return db.Any(c => c.CookieId == cookieId && prodId == prodId);
        }

	}
}
