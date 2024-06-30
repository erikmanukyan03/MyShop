using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductPAV
    {
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductAttributeValueId")]
        public virtual ProductAttributeValue ProductAttributeValue { get; set; }
        public int ProductAttributeValueId { get; set; }


    }
}
