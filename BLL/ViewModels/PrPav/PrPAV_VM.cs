using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.PrPav
{
    public class PrPAV_VM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeValueId { get; set; }
    }
}
