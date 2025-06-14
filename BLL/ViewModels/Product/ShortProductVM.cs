﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class ShortProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Memory { get; set; }
        public ProdColor ProdColor { get; set; }
        public string ShortDescription { get; set; }
        public string? ImageFile { get; set; }
        public int Price { get; set; }
        public int? Discount { get;set; }
        public double FinalPrice { get; set; }


    }
}
