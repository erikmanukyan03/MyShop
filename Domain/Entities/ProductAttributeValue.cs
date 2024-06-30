using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        [ForeignKey("ProductAttributeId")]
        public ProductAttribute ProductAttribute { get; set; }  
        public int ProductAttributeId { get; set; }
        public  ICollection<Product> Products { get; set; }
    }
}
