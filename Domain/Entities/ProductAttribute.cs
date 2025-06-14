﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AttributeType {  get; set; }
        public ICollection<ProductAttributeValue> AttributeValues { get; set; }
    }
}
