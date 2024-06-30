using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class PAVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<PAVVM> Values { get; set; }
    }
}
