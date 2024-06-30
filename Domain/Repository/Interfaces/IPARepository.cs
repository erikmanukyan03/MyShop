using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPARepository
    {
        public void Add(ProductAttribute productAttribute);
        public void Update(ProductAttribute productAttribute);
        public void Delete(int Id);

    }
}
