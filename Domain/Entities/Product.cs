using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string? MainImage { get; set; }
        public string Vendor {  get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public bool HasDiscount { get; set; }
        public string MetaDescription { get; set; }
        public string PageTitle { get; set; }
        public string Slug { get; set; }
        public DateTime? StartDiscount { get; set; }
        public DateTime? EndDiscount { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<ProductVariant> Variants { get; set; }
        public ICollection<ProductAttributeValue> PAVs { get; set; }
		public ICollection<ProductImage> Images { get; set; }
		public int CategoryId { get; set; }
        public int Memory { get; set; }
        public ProdColor ProdColor { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHot { get; set; }
    }
}
