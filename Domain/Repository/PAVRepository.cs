using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class PAVRepository : IPAVRepository
    {
        private readonly MyShopDb _context;
        public PAVRepository(MyShopDb context)
        {
            _context = context;
        }
        public void Add(ProductAttributeValue productAttribute)
        {
            _context.ProductAttributeValues.Add(productAttribute);
        }

        public void Delete(int Id)
        {
            var model = _context.ProductAttributeValues.FirstOrDefault(p => p.Id == Id);
            _context.ProductAttributeValues.Remove(model);
        }

        public void Update(ProductAttributeValue productAttribute)
        {
            var model = _context.ProductAttributeValues.FirstOrDefault(p => p.Id == productAttribute.Id);
            model.Value= productAttribute.Value;
            model.ProductAttributeId = productAttribute.ProductAttributeId;
        }
    }
}
