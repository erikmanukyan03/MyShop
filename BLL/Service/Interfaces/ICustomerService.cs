using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service 
{ 
    public interface ICustomerService
    {
        int Add(CustomerVM model);
        void Delete(int Id);
        CustomerVM GetById(int Id);
    }
}
