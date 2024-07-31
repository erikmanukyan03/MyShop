using BLL.ViewModels;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ProdColor ProdColor { get; set; }
        public List<Atribute_ValueVM>? PAVVMs { get; set; }
        public string ShortDescription { get; set; }
        public int Memory { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string PageTitle { get; set; }
        public string Slug { get; set; }
        public string? MainImage { get; set; }
        public List<ProductImageVM> Images { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsHot { get; set; }
        public double FinalPrice { get; set; }
    }
}
