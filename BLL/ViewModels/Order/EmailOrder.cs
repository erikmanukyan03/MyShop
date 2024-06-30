using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Order
{
    public class EmailOrder
    {
        public OrderVM Details { get; set; }
        public List<CartVM> Carts { get; set; }
    }
}
