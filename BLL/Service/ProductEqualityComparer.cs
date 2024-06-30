using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ProductEqualityComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product? x, Product? y)
        {
            return x!=null && y!=null && x== y;
        }

        public int GetHashCode(Product obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
