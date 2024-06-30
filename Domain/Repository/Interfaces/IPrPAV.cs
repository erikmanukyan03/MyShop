using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IPrPAV
    {
        void AddEdit(int prodId, List<int> list,bool edit);
    }
}
