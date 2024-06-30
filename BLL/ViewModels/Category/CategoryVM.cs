using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ShortProductVM> Products { get; set; }
        public bool IsDeleted { get; set; }
        //public string MetaDescription { get; set; }
        //public string PageTitle { get; set; }
        //public string Slug { get; set; }
    }
}
