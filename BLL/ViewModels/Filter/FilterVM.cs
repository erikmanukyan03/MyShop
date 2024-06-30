using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class FilterVM
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public List<string> RAM { get; set; }
        public List<string> Memory { get; set; }
        public List<string> ScreenSize { get; set; }
        public List<string> Color { get; set; }

    }
}
