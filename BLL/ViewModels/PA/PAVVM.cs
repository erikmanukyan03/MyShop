using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class PAVVM
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ProductAttributeId { get; set; }
    }
}
