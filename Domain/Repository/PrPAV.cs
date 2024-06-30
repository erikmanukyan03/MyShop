using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class PrPAV : IPrPAV
    {
        private readonly MyShopDb _db;
        public PrPAV(MyShopDb db)
        {
            _db = db;
        }

        public void AddEdit(int prodId, List<int> list,bool edit)
        {
            var prod = _db.Products;
            var c=prod.Include(p=>p.PAVs).FirstOrDefault(p => p.Id == prodId);
            var b = _db.ProductAttributeValues;
            var d = b.Include(p => p.Products).Where(p => list.Contains(p.Id)).ToList();
            if (edit == true)
            {
                c.PAVs.Clear(); ///////////////chisht chi ashxatm
            }            
            foreach (var v in d )
            {  
                c.PAVs.Add(v);
            }

        }
    }
}
