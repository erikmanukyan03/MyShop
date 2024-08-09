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
    public static class CompareExtention
    {
        public static List<Compare> GetAll(this DbSet<Compare> db,string cookieId)
        {
            return db.Where(c=>c.CookieId==cookieId).Include(c=>c.Products).ToList();
        }
        public static List<Compare> GetByCookie(this DbSet<Compare> db, string cookieId)
        {
            return db.Where(c => c.CookieId == cookieId).Include(c => c.Products).ToList();
        }
        public static Compare GetForEdit(this DbSet<Compare> db,int Id)
        {
            return db.FirstOrDefault(c => c.Id == Id);
        }
    }
}
