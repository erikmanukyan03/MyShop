using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPAVRepository
    {
        public void Add(ProductAttributeValue productAttribute);
        public void Update(ProductAttributeValue productAttribute);
        public void Delete(int Id);
    }
}
