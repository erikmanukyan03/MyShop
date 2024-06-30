using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class PARepository : IPARepository
    {
        private readonly MyShopDb _context;
        public PARepository(MyShopDb context)
        {
            _context = context;
        }
        public void Add(ProductAttribute productAttribute)
        {
            _context.ProductAttributes.Add(productAttribute);
        }

        public void Delete(int Id)
        {
            var model = _context.ProductAttributes.FirstOrDefault(p => p.Id ==Id);
            _context.ProductAttributes.Remove(model);
        }

        public void Update(ProductAttribute productAttribute)
        {
           var model= _context.ProductAttributes.FirstOrDefault(p => p.Id == productAttribute.Id);
            model.Name= productAttribute.Name;
            model.AttributeValues= productAttribute.AttributeValues;
        }
    }
}
